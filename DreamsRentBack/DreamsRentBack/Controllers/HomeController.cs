using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Services;
using DreamsRentBack.Utilities;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Bodies = _context.Bodys.Include(b=>b.Cars).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();
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
                })
                .Take(9)
                .ToList();
        
            return View();
        }

        public IActionResult Subscribtion(string email)
        {
            Subscribe subscribe = new()
            {
                Email = email,
            };

            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
