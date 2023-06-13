using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public CityController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Cities()
        {
            List<City> cities = _context.Cities.ToList();
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(City createCity)
        {
            City city = new()
            {
                Name = createCity.Name,
            };

            _context.Cities.Add(city);
            _context.SaveChanges();
            return RedirectToAction(nameof(Cities));
        }

        public IActionResult Edit(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);
            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(City editCity)
        {
            City city = _context.Cities.FirstOrDefault(b => b.Id == editCity.Id);

            city.Name = editCity.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(Cities));
        }
    }
}
