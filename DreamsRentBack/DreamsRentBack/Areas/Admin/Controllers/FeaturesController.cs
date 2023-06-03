using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeaturesController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public FeaturesController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Features()
        {
            List<CarFeatures> carFeatures = _context.CarsFeatures.ToList();

            return View(carFeatures);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarFeatures createFeature)
        {
            CarFeatures carFeatures = new()
            {
                Name = createFeature.Name,
            };

            _context.CarsFeatures.Add(createFeature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Features));
        }

        public IActionResult Edit(int id)
        {
            CarFeatures carFeatures = _context.CarsFeatures.FirstOrDefault(b => b.Id == id);

            return View(carFeatures);
        }

        [HttpPost]
        public IActionResult Edit(CarFeatures editCarFeatures)
        {
            CarFeatures carFeatures = _context.CarsFeatures.FirstOrDefault(b => b.Id == editCarFeatures.Id);

            carFeatures.Name = editCarFeatures.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(Features));
        }
    }
}
