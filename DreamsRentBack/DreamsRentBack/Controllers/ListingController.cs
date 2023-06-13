using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace DreamsRentBack.Controllers
{
    public class Listingcontroller : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public Listingcontroller(DreamsRentDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(string? streetName, DateTime pickupDate, DateTime returnDate, int page = 1) 
        {

            ViewBag.Brands = _context.Brands.Include(b=>b.Models).OrderBy(b=>b.Name).ToList();
            ViewBag.Bodies = _context.Bodys.OrderBy(b=>b.Name).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();
            ViewBag.User = _context.Users
               .Include(u => u.Wishlist)
                   .ThenInclude(w => w.wishlistItems)
                       .FirstOrDefault(u => u.UserName == User.Identity.Name);
            List<Car> expensives = _context.Cars.OrderBy(c=>c.Price).ToList();
            ViewBag.Expensive = expensives.First().Price;

            ViewBag.TotalPage = Math.Ceiling((double)_context.Cars.Count() / 6);
            ViewBag.CurrentPage = page;

            List<CarExploreVM> cars = _context.Cars
                .Include(c => c.CarPhotos)
                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                            .Include(c => c.Company)
                            .Where(c=>c.CarConfirmation == CarConfirmation.Confirmed)
                                .OrderByDescending(c => c.Id)
            .Select(c=> new CarExploreVM
            {
                Id = c.Id,
                CarPhotos = c.CarPhotos,
                Brand = c.Brand,
                ModelId = c.ModelId,
                Price = c.Price,
                Rating = c.Rating,
                Transmission = c.Transmission,
                Speed = c.Speed,
                FuelType = c.FuelType,
                Engine = c.Engine,
                Year = c.Year,
                Capacity = c.Capacity,
                Company = c.Company,
            })
            .Skip((page - 1) * 6).Take(6)
            .ToList();

            if (!string.IsNullOrEmpty(streetName))
            {
                List<CarExploreVM> filterCars = SearchByStatus(streetName, pickupDate, returnDate, page);
                if (filterCars.Count() == 0)
                {
                    List<CarExploreVM> carsEmpty = new();
                    return View(carsEmpty);
                }
                return View(filterCars);
            }

            return View(cars);
        }

        [HttpPost]
        public IActionResult Index(int[] selectedBrands, int[] selectedBodies, int selectedCapacity, int selectedPrice, int selectedRating)
        {
            ViewBag.Brands = _context.Brands.Include(b => b.Models).OrderBy(b => b.Name).ToList();
            ViewBag.Bodies = _context.Bodys.OrderBy(b => b.Name).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r => r.Comment).ToList();
            ViewBag.User = _context.Users
                .Include(u => u.Wishlist)
                    .ThenInclude(w => w.wishlistItems)
                        .FirstOrDefault(u => u.UserName == User.Identity.Name);
            List<Car> expensives = _context.Cars.OrderBy(c => c.Price).ToList();
            ViewBag.Expensive = expensives.First().Price;
            IQueryable<Car>? cars = _context.Cars
                .Include(c => c.CarPhotos)
                                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                                        .Include(c => c.Company)
                                            .Include(c=>c.Comments).ThenInclude(c=>c.Rating)
                                            .Include(c => c.Transmission)
                                            .Include(c => c.FuelType)
                                                .Include(c => c.Engine)
                                                 .OrderByDescending(c => c.Id)
                                                    .Take(6);

            if (selectedBrands.Count() != 0)
            {
                cars = cars.Where(c => selectedBrands.Contains(c.BrandId)).AsQueryable();
            }
            if (selectedBodies.Count() != 0)
            {
                cars = cars.Where(c => selectedBodies.Contains(c.BodyId)).AsQueryable();
            }
            if (selectedCapacity != 0)
            {
                switch (selectedCapacity) 
                {
                    case 1:
                        cars = cars.Where(c => c.Capacity >= 2 && c.Capacity <= 4).AsQueryable();
                        break;
                    case 2:
                        cars = cars.Where(c => c.Capacity >= 4 && c.Capacity <= 6).AsQueryable();
                        break;
                    case 3:
                        cars = cars.Where(c => c.Capacity >= 6 && c.Capacity <= 10).AsQueryable();
                        break;
                }
            }
            if (selectedPrice != 0)
            {
                switch (selectedPrice)
                {
                    case 1:
                        cars = cars.Where(c => c.Price > 0 && c.Price <= 50).AsQueryable();
                        break;
                    case 2:
                        cars = cars.Where(c => c.Price >= 50 && c.Price <= 150).AsQueryable();
                        break;
                    case 3:
                        cars = cars.Where(c => c.Price >= 150 && c.Price <= 300).AsQueryable();
                        break;
                    case 4:
                        cars = cars.Where(c => c.Price >= 300 && c.Price <= 500).AsQueryable();
                        break;
                    case 5:
                        cars = cars.Where(c => c.Price >= 500 && c.Price <= 1000).AsQueryable();
                        break;
                    case 6:
                        cars = cars.Where(c => c.Price >= 1000).AsQueryable();
                        break;
                }
            }

            List<Rating> ratingList = _context.Ratings.Include(r => r.Comment).ThenInclude(c=>c.Car).ToList();

            if (selectedRating > 0)
            {
                switch (selectedRating)
                {
                    case 1:
                        IEnumerable<Car> filteredCars1 = cars.AsEnumerable()
                            .Where(c => (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) >= 1 && 
                                (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) < 2);
                        cars = filteredCars1.AsQueryable();
                        break;
                    case 2:
                        IEnumerable<Car> filteredCars2 = cars.AsEnumerable()
                            .Where(c => (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) >= 2 && 
                                (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) < 3);
                        cars = filteredCars2.AsQueryable();
                        break;
                    case 3:
                        IEnumerable<Car> filteredCars3 = cars.AsEnumerable()
                            .Where(c => (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) >= 3 && 
                                (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) < 4);
                        cars = filteredCars3.AsQueryable();
                        break;
                    case 4:
                        IEnumerable<Car> filteredCars4 = cars.AsEnumerable()
                            .Where(c => (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) >= 4 && 
                                (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) < 5);
                        cars = filteredCars4.AsQueryable();
                        break;
                    case 5:
                        IEnumerable<Car> filteredCars5 = cars.AsEnumerable()
                            .Where(c => (c.Rating / ratingList.FindAll(r => r.Comment.CarId == c.Id).Count()) == 5);
                        cars = filteredCars5.AsQueryable();
                        break;
                }
            }


            List<CarExploreVM> list = cars.Select(c=> new CarExploreVM
            {
                Id = c.Id,
                CarPhotos = c.CarPhotos,
                Brand = c.Brand,
                ModelId = c.ModelId,
                Price = c.Price,
                Rating = c.Rating,
                Transmission = c.Transmission,
                Speed = c.Speed,
                FuelType = c.FuelType,
                Engine = c.Engine,
                Year = c.Year,
                Capacity = c.Capacity,
                Company = c.Company,

            }).ToList();
            return PartialView("_partialListing", list);
        }

        private List<CarExploreVM> SearchByStatus(string streetName, DateTime pickupDate, DateTime returnDate, int page=1)
        {
            ViewBag.Brands = _context.Brands.Include(b=>b.Models).OrderBy(b=>b.Name).ToList();
            ViewBag.Bodies = _context.Bodys.OrderBy(b=>b.Name).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();
            List<Car> expensives = _context.Cars.OrderBy(c => c.Price).ToList();
            ViewBag.Expensive = expensives.First().Price;

            ViewBag.TotalPage = Math.Ceiling((double)_context.Cars.Count() / 6);
            ViewBag.CurrentPage = page;

            string[] streetNameSplit = streetName.Split(", ");

            if (streetNameSplit.Length != 2)
            {
                List<CarExploreVM> carsVms = _context.Cars
                .Include(c => c.CarPhotos)
                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                            .Include(c => c.Company)
                            .Where(c => c.Id == 0)
                                .OrderByDescending(c => c.Id)
                .Select(c => new CarExploreVM
                {
                    Id = c.Id,
                    CarPhotos = c.CarPhotos,
                    Brand = c.Brand,
                    ModelId = c.ModelId,
                    Price = c.Price,
                    Rating = c.Rating,
                    Transmission = c.Transmission,
                    Speed = c.Speed,
                    FuelType = c.FuelType,
                    Engine = c.Engine,
                    Year = c.Year,
                    Capacity = c.Capacity,
                    Company = c.Company,
                })
                .Skip((page - 1) * 6).Take(6)
                .ToList();

                return carsVms;
            }


            Street? street = _context.Streets
                .Include(s=>s.City)
                    .FirstOrDefault(s => s.Name.Equals(streetNameSplit[1]));

            if (street == null)
            {
                List<CarExploreVM> carsVms = _context.Cars
                .Include(c => c.CarPhotos)
                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                            .Include(c => c.Company)
                            .Where(c => c.Id == 0)
                                .OrderByDescending(c => c.Id)
                .Select(c => new CarExploreVM
                {
                    Id = c.Id,
                    CarPhotos = c.CarPhotos,
                    Brand = c.Brand,
                    ModelId = c.ModelId,
                    Price = c.Price,
                    Rating = c.Rating,
                    Transmission = c.Transmission,
                    Speed = c.Speed,
                    FuelType = c.FuelType,
                    Engine = c.Engine,
                    Year = c.Year,
                    Capacity = c.Capacity,
                    Company = c.Company,
                })
                .Skip((page - 1) * 6).Take(6)
                .ToList();

                return carsVms;
            }

            PickupLocation? pickupLocation = _context.PickupLocations
                .FirstOrDefault(p => p.StreetId == street.Id);

            List<CarExploreVM> carExploreVMs = new();

            if (pickupLocation == null)
            {
                return carExploreVMs;
            }

            List<Car> cars = _context.Cars.Include(c => c.Company)
                    .Include(c => c.Brand).ThenInclude(b => b.Models)
                        .Include(c => c.Body)
                        .Include(c => c.Transmission)
                            .Include(c => c.FuelType)
                            .Include(c => c.CarPhotos)
                                .Include(c => c.ServicesAndCars).ThenInclude(sc => sc.ExtraService)
                                .Include(c => c.FeaturesAndCars).ThenInclude(fc => fc.CarFeatures)
                                    .Include(c => c.Drivetrian)
                                    .Include(c => c.AirCondition)
                                        .Include(c => c.Brake)
                                        .Include(c => c.Engine)
                                            .Include(c => c.Comments).ThenInclude(c => c.Rating)
                                            .Where(c=>c.Company.companyPickupLocations
                                                .Any(p=>p.PickupLocation.Id == pickupLocation.Id))
                                            .Where(c=> (pickupDate < c.PickupDate && returnDate < c.PickupDate) || 
                                                       (pickupDate > c.ReturnDate && returnDate > c.ReturnDate) || 
                                                       (c.PickupDate == null && c.ReturnDate == null))
                                            .OrderByDescending(c => c.Id)
                                            .Skip((page - 1) * 6).Take(6)
                                            .ToList();

            foreach (Car item in cars)
            {
                CarExploreVM carExploreVM = new()
                {
                    Id = item.Id,
                    CarPhotos = item.CarPhotos,
                    Brand = item.Brand,
                    ModelId = item.ModelId,
                    Price = item.Price,
                    Rating = item.Rating,
                    Transmission = item.Transmission,
                    Speed = item.Speed,
                    FuelType = item.FuelType,
                    Engine = item.Engine,
                    Year = item.Year,
                    Capacity = item.Capacity,
                    Company = item.Company,
                };
                carExploreVMs.Add(carExploreVM);
            }

            return carExploreVMs;
        }
    }
}
