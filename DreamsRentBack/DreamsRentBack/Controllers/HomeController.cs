using DreamsRentBack.DAL;
using DreamsRentBack.Services;
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
            ViewBag.Likes = _context.Likes.Include(l=>l.User).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();

            ViewBag.Cars = _context.Cars
                .Include(c=>c.CarPhotos)
                    .Include(c=>c.Likes)
                        .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                            .Include(c=>c.Company).ThenInclude(c=>c.User)
                                .OrderByDescending(c => c.Id)
                .Select(c=> new CarExploreVM
                {
                    Id = c.Id,
                    CarPhotos = c.CarPhotos,
                    Likes = c.Likes,
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
    }
}
