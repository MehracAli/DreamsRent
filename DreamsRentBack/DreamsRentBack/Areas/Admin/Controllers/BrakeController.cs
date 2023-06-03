using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrakeController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public BrakeController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult BrakeTypes()
        {
            List<Brake> brakeTypes = _context.Brakes.ToList();

            return View(brakeTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brake createBrake)
        {
            Brake brake = new()
            {
                Name = createBrake.Name,
            };

            _context.Brakes.Add(brake);
            _context.SaveChanges();
            return RedirectToAction(nameof(BrakeTypes));
        }

        public IActionResult Edit(int id)
        {
            Brake brake = _context.Brakes.FirstOrDefault(b => b.Id == id);

            return View(brake);
        }

        [HttpPost]
        public IActionResult Edit(Brake edtiBrake)
        {
            Brake brake = _context.Brakes.FirstOrDefault(b => b.Id == edtiBrake.Id);

            brake.Name = edtiBrake.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(BrakeTypes));
        }
    }
}
