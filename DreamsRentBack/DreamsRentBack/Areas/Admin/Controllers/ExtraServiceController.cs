using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExtraServiceController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public ExtraServiceController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult ExtraServices()
        {
            List<ExtraService> extraServices = _context.ExtraServices.ToList();

            return View(extraServices);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExtraService createExtraService)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ExtraService extraService = new()
            {
                Name = createExtraService.Name,
                Price = createExtraService.Price,
            };

            _context.ExtraServices.Add(extraService);
            _context.SaveChanges();
            return RedirectToAction(nameof(ExtraServices));
        }

        public IActionResult Edit(int id)
        {
            ExtraService extraService = _context.ExtraServices.FirstOrDefault(b => b.Id == id);

            return View(extraService);
        }

        [HttpPost]
        public IActionResult Edit(ExtraService editExtraService)
        {
            ExtraService extraService = _context.ExtraServices.FirstOrDefault(b => b.Id == editExtraService.Id);

            extraService.Name = editExtraService.Name;
            extraService.Price = editExtraService.Price;

            _context.SaveChanges();
            return RedirectToAction(nameof(ExtraServices));
        }
    }
}
