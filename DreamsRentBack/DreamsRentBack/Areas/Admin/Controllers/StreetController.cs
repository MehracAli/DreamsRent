using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StreetController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public StreetController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Streets()
        {
            List<Street> streets = _context.Streets.Include(s=>s.City).ToList();
            return View(streets);
        }

        public IActionResult Create()
        {
            ViewBag.Cities = _context.Cities.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Street createStreet)
        {
            Street street = new()
            {
                Name = createStreet.Name,
                CityId = createStreet.CityId,
            };

            _context.Streets.Add(street);
            _context.SaveChanges();
            return RedirectToAction(nameof(Streets));
        }
    }
}
