using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EngineController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public EngineController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Engines()
        {
            List<Engine> engines = _context.Engines.ToList();

            return View(engines);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Engine createEngine)
        {
            Engine engine = new()
            {
                HorsePower = createEngine.HorsePower,
            };

            _context.Engines.Add(engine);
            _context.SaveChanges();
            return RedirectToAction(nameof(Engines));
        }

        public IActionResult Edit(int id)
        {
            Engine engine = _context.Engines.FirstOrDefault(b => b.Id == id);

            return View(engine);
        }

        [HttpPost]
        public IActionResult Edit(Engine editEngine)
        {
            Engine engine = _context.Engines.FirstOrDefault(b => b.Id == editEngine.Id);

            engine.HorsePower = editEngine.HorsePower;

            _context.SaveChanges();
            return RedirectToAction(nameof(Engines));
        }
    }
}
