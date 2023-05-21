using DreamsRentBack.DAL;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Controllers
{
    public class HomeController : Controller
    {
        public DreamsRentDbContext _context;

        public HomeController(DreamsRentDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Bodies = _context.Bodys.Include(b=>b.Cars).ToList();
            ViewBag.Likes = _context.Likes.Include(l=>l.User).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();

            ViewBag.Cars = _context.Cars
                .Include(c=>c.CarPhotos)
                    .Include(c=>c.Likes)
                        .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                            .Include(c=>c.Company)
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
                })
                .Take(9)
                .ToList();
        
            return View();
        }
    }
}
