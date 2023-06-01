using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Controllers
{
    public class RentController : Controller
    {
        public DreamsRentDbContext _context;

        public RentController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout(
            int carId,
            int companyId,
            string pickLocation,
            string dropLocation,
            DateTime pickDate,
            DateTime dropDate)
        {

            ViewBag.PayCards = _context.PayCardTypes.ToList();

            Company? company = _context.Companies
                .Include(c=>c.companyPickupLocations)
                    .Include(c=>c.companyDropoffLocations)
                        .FirstOrDefault(c=>c.Id == companyId);

            if (pickLocation == null && dropLocation == null)
            {
                return RedirectToAction("CarDetail", "Detail", new { Id = carId });
            }

            if (!pickLocation.Contains(", ") || !dropLocation.Contains(", "))
            {
                return RedirectToAction("CarDetail", "Detail", new { Id = carId });
            }

            string pickCity = pickLocation.Split(", ")[0];
            string pickStreet = pickLocation.Split(", ")[1];

            City? cityPick = _context.Cities.FirstOrDefault(c=>c.Name.Equals(pickCity));
            Street? streetPick = _context.Streets.FirstOrDefault(s=>s.Name.Equals(pickStreet));
            
            if (cityPick == null && streetPick == null)
            {
                return RedirectToAction("CarDetail", "Detail", new { Id = carId });
            }

            PickupLocation? pickupLocation = _context.PickupLocations
                .FirstOrDefault(p=>p.CityId == cityPick.Id && p.StreetId == streetPick.Id);

            string dropCity = dropLocation.Split(", ")[0];
            string dropStreet = dropLocation.Split(", ")[1];

            City? cityDrop = _context.Cities.FirstOrDefault(c => c.Name.Equals(dropCity));
            Street? streetDrop = _context.Streets.FirstOrDefault(s => s.Name.Equals(dropStreet));

            DropoffLocation? dropoffLocation = _context.DropoffLocations
                .FirstOrDefault(d => d.CityId == cityDrop.Id && d.StreetId == streetDrop.Id);

            if (company.companyPickupLocations.Any(cp=>cp.PickupLocationId == pickupLocation.Id) && 
                company.companyDropoffLocations.Any(cd=>cd.DropoffLocationId == dropoffLocation.Id))
            {
                Car? car = _context.Cars.FirstOrDefault(c => c.Id == carId);

                bool CarStatus = IsCarRentedBetweenDates(car, pickDate, dropDate);

                if (!CarStatus)
                {
                    CarCheckoutVM carCheckoutVM = new()
                    {
                        Id = car.Id,
                        PickLocation = pickCity + " " + pickStreet,
                        DropLocation = dropCity + " " + dropStreet,
                        PickDate = pickDate,
                        DropDate = dropDate,
                        Price = car.Price,
                        User = _context.Users.Include(u=>u.PayCard).FirstOrDefault(u=>u.UserName == User.Identity.Name)
                    };
                    return View(carCheckoutVM);
                }
            }

            return View();
        }

        public bool IsCarRentedBetweenDates(Car car, DateTime pickDate, DateTime dropDate)
        {
            if (car.CarStatus == CarStatus.Rented)
            {
                if (car.PickupDate >= pickDate && car.PickupDate <= dropDate)
                {
                    return true;
                }

                if (car.ReturnDate >= pickDate && car.ReturnDate <= dropDate)
                {
                    return true;
                }

                if (car.PickupDate <= pickDate && car.ReturnDate >= dropDate)
                {
                    return true;
                }
            }

            return false;
        }

        public IActionResult OrderConfirmation(
            string UserName,
            int carId,
            string comment,
            int payCardTypeId,
            string cardNumber,
            string holderName,
            string holderSurname,
            string date,
            string cvv,
            string pickLocation,
            string dropLocation,
            DateTime pickDate,
            DateTime dropDate)
        {
            User user = _context.Users.Include(u=>u.PayCard).FirstOrDefault(u=> u.UserName == UserName);
            Car car = _context.Cars.Include(c=>c.Brand).FirstOrDefault(c=>c.Id == carId);
            PayCardType cardType = _context.PayCardTypes.FirstOrDefault(p=>p.Id == payCardTypeId);

            if (user.PayCard == null)
            {
                PayCard payCard = new()
                {
                    CardNumber = cardNumber.Replace(" ", ""),
                    HolderName = holderName.ToUpper(),
                    HolderSurname = holderSurname.ToUpper(),
                    Date = date,
                    cvv = cvv,
                    PayCardType = cardType,
                    User = user
                };
                _context.PayCards.Add(payCard);
            }

            car.PickupDate = pickDate;
            car.ReturnDate = dropDate;

            Model model = _context.Models.FirstOrDefault(m=>m.Id  == car.ModelId);

            CarOrderVM carOrderVM = new()
            {
                Id = car.Id,
                BrandName = car.Brand.Name,
                ModelName = model.Name,
                PickupLocation = pickLocation,
                DropoffLocation = dropLocation,
                PickupDate = pickDate,
                DropoffDate = dropDate,
                Price = car.Price,
                User = user,
                Comment = comment
            };

            _context.SaveChanges();
            return View(carOrderVM);
        }

        public IActionResult AlreadyOrdered()
        {
            return View();
        }
        public IActionResult SuccesfullyOrdered()
        {
            return View();
        }

        public IActionResult PlaceOrder(int carId)
        {
            Car car = _context.Cars.Include(c=>c.Company).FirstOrDefault(c => c.Id == carId);

            User user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            Order order = _context.Orders.Include(o=>o.Car).FirstOrDefault(o => o.User.UserName == User.Identity.Name);

            if (order != null )
            {
                if (order.Car.Id == car.Id)
                {
                    return RedirectToAction("AlreadyOrdered", "Rent");    
                }
            }

            car.CarStatus = CarStatus.Pending;
            Order newOrder = new()
            {
                User = user,
                Car = car,
                Company = car.Company,
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return RedirectToAction("SuccesfullyOrdered", "Rent");
        }

        public IActionResult AcceptOrder(int carId, string userId, int orderId)
        {
            User user = _context.Users.Include(u=>u.PayCard).FirstOrDefault(u => u.Id == userId);
            Car car = _context.Cars.Include(c=>c.Company).FirstOrDefault(c => c.Id == carId);
            Order order = _context.Orders.FirstOrDefault(o=>o.Id == orderId);
            Company company = _context.Companies.Include(c=>c.User).FirstOrDefault(c => c.Id == car.CompanyId);

            car.CarStatus = CarStatus.Rented;
            Rent rent = new()
            {
                User = user,
                Car = car,
                Company = car.Company
            };
            Booking booking = new()
            {
                User = user,
                car = car,
                company = company
            };

            user.PayCard.Balance -= car.Price;
            user.Rents.Add(rent);
            company.Bookings.Add(booking);
            _context.Orders.Remove(order);

            _context.SaveChanges();
            return RedirectToAction("CompanyAccount", "Account", new {UserName = company.User.UserName});
        }

        public IActionResult RejectOrder(int carId, string userId, int orderId)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == userId);
            Car car = _context.Cars.Include(c => c.Company).FirstOrDefault(c => c.Id == carId);
            Order order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            Company company = _context.Companies.Include(c => c.User).FirstOrDefault(c => c.Id == car.CompanyId);

            car.CarStatus = CarStatus.Available;
            car.PickupDate = null;
            car.ReturnDate = null;

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
        }
    }
}
