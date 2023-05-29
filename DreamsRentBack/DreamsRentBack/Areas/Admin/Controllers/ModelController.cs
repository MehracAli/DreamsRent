using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModelController : Controller
    {
        public DreamsRentDbContext _context { get; set; }
        public ModelController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Models()
        {
            List<Model> models = _context.Models.Include(m=>m.Brand).ToList(); 
            return View(models);
        }
        
        public IActionResult Create() 
        {
            ViewBag.Brands = _context.Brands.ToList();
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult Creating(Model createModel)
        {
            Model model = new()
            {
                Name = createModel.Name,
            };

            Brand brand = _context.Brands.FirstOrDefault(b=>b.Id == createModel.BrandId);

            model.Brand = brand;

            _context.Models.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Models));
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.Brands = _context.Brands.ToList();
            Model model = _context.Models.FirstOrDefault(m => m.Id == Id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Editing(Model edited)
        {
            Model model = _context.Models.FirstOrDefault(m=>m.Id == edited.Id);
            
            model.Name = edited.Name;
            model.BrandId = edited.BrandId;

            _context.SaveChanges();

            return RedirectToAction(nameof(Models));
        }

        public ActionResult Delete(int Id)
        {
            Model model = _context.Models.Include(m=>m.Brand).FirstOrDefault(m => m.Id == Id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Deleting(Model deleteModel)
        {
            Model model = _context.Models.FirstOrDefault(m=>m.Id==deleteModel.Id);

            List<Car> cars = _context.Cars.Where(car=>car.ModelId==model.Id).ToList();

            foreach (Car car in cars)
            {
                _context.Cars.Remove(car);
            }

            _context.Models.Remove(model);
            _context.SaveChanges();

            return RedirectToAction(nameof(Models));
        }
    }
}
