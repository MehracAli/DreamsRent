using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FuelTypeController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public FuelTypeController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult FuelTypes()
        {
            List<FuelType> fuelTypes = _context.FuelTypes.ToList();

            return View(fuelTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FuelType createFuelType)
        {
            FuelType fuelType = new()
            {
                Name = createFuelType.Name,
            };

            _context.FuelTypes.Add(fuelType);
            _context.SaveChanges();
            return RedirectToAction(nameof(FuelTypes));
        }

        public IActionResult Edit(int id)
        {
            FuelType fuelType = _context.FuelTypes.FirstOrDefault(b => b.Id == id);

            return View(fuelType);
        }

        [HttpPost]
        public IActionResult Edit(FuelType createFuelType)
        {
            FuelType fuelType = _context.FuelTypes.FirstOrDefault(b => b.Id == createFuelType.Id);

            fuelType.Name = createFuelType.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(FuelTypes));
        }
    }
}
