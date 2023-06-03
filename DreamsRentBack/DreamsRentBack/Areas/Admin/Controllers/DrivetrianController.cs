using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DrivetrianController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public DrivetrianController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Drivetrians()
        {
            List<Drivetrian> drivetrians = _context.Drivetrians.ToList();

            return View(drivetrians);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Drivetrian createDrivetrian)
        {
            Drivetrian drivetrian = new()
            {
                Name = createDrivetrian.Name,
            };

            _context.Drivetrians.Add(drivetrian);
            _context.SaveChanges();
            return RedirectToAction(nameof(Drivetrians));
        }

        public IActionResult Edit(int id)
        {
            Drivetrian drivetrian = _context.Drivetrians.FirstOrDefault(b => b.Id == id);

            return View(drivetrian);
        }

        [HttpPost]
        public IActionResult Edit(Drivetrian editDrivetrian)
        {
            Drivetrian drivetrian = _context.Drivetrians.FirstOrDefault(b => b.Id == editDrivetrian.Id);

            drivetrian.Name = editDrivetrian.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(Drivetrians));
        }
    }
}
