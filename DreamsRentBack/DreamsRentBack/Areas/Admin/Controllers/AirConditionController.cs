using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AirConditionController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public AirConditionController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult AirConditions()
        {
            List<AirCondition> airConditions = _context.AirConditions.ToList();

            return View(airConditions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AirCondition createAirCondition)
        {
            AirCondition airCondition = new()
            {
                Name = createAirCondition.Name,
            };

            _context.AirConditions.Add(airCondition);
            _context.SaveChanges();

            return RedirectToAction(nameof(AirConditions));
        }

        public IActionResult Edit(int Id)
        {
            AirCondition airCondition = _context.AirConditions.FirstOrDefault(x => x.Id == Id);

            return View(airCondition);
        }

        [HttpPost]
        public IActionResult Edit(AirCondition editAirCondition)
        {
            AirCondition airCondition = _context.AirConditions.FirstOrDefault(b => b.Id == editAirCondition.Id);

            airCondition.Name = editAirCondition.Name;

            _context.SaveChanges();
            return RedirectToAction(nameof(AirConditions));
        }
    }
}
