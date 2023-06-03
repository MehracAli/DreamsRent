using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransmissionController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public TransmissionController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Transmissions()
        {
            List<Transmission> transmissions = _context.Transmissions.ToList();

            return View(transmissions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transmission createTransmission)
        {
            Transmission transmission = new()
            {
                Name = createTransmission.Name,
            };

            _context.Transmissions.Add(transmission);
            _context.SaveChanges();
            return RedirectToAction(nameof(Transmissions));
        }

        public IActionResult Edit(int id)
        {
            Transmission transmission = _context.Transmissions.FirstOrDefault(b => b.Id == id);

            return View(transmission);
        }

        [HttpPost]
        public IActionResult Edit(Transmission editTransmission)
        {
            Transmission transmission = _context.Transmissions.FirstOrDefault(b => b.Id == editTransmission.Id);

            transmission.Name = editTransmission.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(Transmissions));
        }
    }
}
