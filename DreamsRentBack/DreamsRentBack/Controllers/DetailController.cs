using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.ViewModels.Car;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Controllers
{
    public class DetailController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public DetailController(DreamsRentDbContext context)
        {
            _context = context;
        }

        #region CarDetail
        public IActionResult CarDetail(int Id)
        {
            if (Id != 0)
            {
                Car car = _context.Cars.FirstOrDefault(c => c.Id == Id);

                CarDetailVM carDetailVM = new()
                {

                };

                if (car == null) { return RedirectToAction("NotFound", "Error"); }

                return View(car);
            }
            return RedirectToAction("NotFound", "Error");
        }
        #endregion
    }
}
