using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Migrations;
using DreamsRentBack.Services;
using DreamsRentBack.Utilities;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DreamsRentBack.Controllers
{
    public class HomeController : Controller
    {
        public DreamsRentDbContext _context;
        readonly ChatService _chatService;

        public HomeController(DreamsRentDbContext context, ChatService chatService)
        {
            _context = context;
            _chatService = chatService;
        }

        public async Task<IActionResult> Index()
        {
            CheckRents();

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Bodies = _context.Bodys.Include(b=>b.Cars).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();
            ViewBag.HappyUsers = _context.Users
                .Where(u=>u.Comments.FirstOrDefault(c=>c.Rating.Point > 4) != null).ToList();
            
            ViewBag.ContactUs = _context.ContactUsMessages.ToList();
            ViewBag.Bookings = _context.Bookings.ToList();
            ViewBag.User = _context.Users
                .Include(u=>u.Wishlist)
                    .ThenInclude(w=>w.wishlistItems)
                        .FirstOrDefault(u=>u.UserName == User.Identity.Name);

            ViewBag.Cars = _context.Cars
                .Include(c=>c.CarPhotos)
                        .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                            .Include(c=>c.Company).ThenInclude(c=>c.User)
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
                    Availability = c.Availability,
                    Comments = c.Comments,
                })
                .Take(9)
                .ToList();
        
            return View();
        }

        public IActionResult Subscribtion(string email)
        {
            Entities.ClientModels.Subscribe subscribe = new()
            {
                Email = email,
            };

            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public void CheckRents()
        {
            List<Rent> rents = _context.Rents
                .Include(r=>r.User)
                .Include(r=>r.Car).ThenInclude(r=>r.Brand).ThenInclude(b=>b.Models)
                .Include(r=>r.Car).ThenInclude(r=>r.Company)
                .ToList();

            foreach (var item in rents)
            {
                if (!item.TimeIsOver)
                {
                    if (item.DropDate <= DateTime.Now)
                    {
                        MailMessage message = new MailMessage();
                        message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
                        message.To.Add(new MailAddress(item.User.Email));
                        message.Subject = "Your rental period is over!";
                        message.Body = string.Empty;
                        message.Body = $"Last day of renting {item.DropDate.ToString("D")}" +" "+
                        $"{item.Car.Brand.Name + " " + item.Car.Brand.Models.FirstOrDefault(m => m.Id == item.Car.ModelId).Name + " from " + item.Car.Company.CompanyName + " link: https://localhost:7260/Detail/CarDetail/" + item.Car.Id}" +
                        $". Please return car to chosen drop location.";

                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.Credentials = new NetworkCredential("dreamsrentofficial@gmail.com", "cxpstlrytkzgyrdk");
                        smtpClient.Send(message);

                        Booking booking = _context.Bookings.FirstOrDefault(b => b.Id == item.Id);
                        Car car = _context.Cars.FirstOrDefault(c=>c.Id == item.CarId);
                        car.PickupDate = null;
                        car.ReturnDate = null;
                        car.CarStatus = CarStatus.Available;
                        item.TimeIsOver = true;
                        booking.TimeIsOver = true;

                        _context.SaveChanges();
                    }
                }
            }
        }

        public IActionResult FilterBrands(int brandId)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Bodies = _context.Bodys.Include(b => b.Cars).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r => r.Comment).ToList();
            ViewBag.User = _context.Users
                .Include(u => u.Wishlist)
                    .ThenInclude(w => w.wishlistItems)
                        .FirstOrDefault(u => u.UserName == User.Identity.Name);

            ViewBag.Cars = _context.Cars.Include(c => c.CarPhotos)
                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                            .Include(c => c.Company).ThenInclude(c => c.User)
                                .Where(c => c.CarConfirmation == CarConfirmation.Confirmed)
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
                    Availability = c.Availability,
                })
                .Take(9)
                .Where(c=>c.Brand.Id == brandId)
                .ToList();

            var cars = ViewBag.Cars;

            return PartialView("_partialHomeCars", cars);
        }
    }
}
