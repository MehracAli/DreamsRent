using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities.Extentions;
using DreamsRentBack.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        public DreamsRentDbContext _context { get; set; }
        private readonly IWebHostEnvironment _env;

        public BrandController(DreamsRentDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Brands()
        {
            List<Brand> brands = _context.Brands.ToList(); 

            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Creating(Brand createBrand)
        {
            Brand brand = new()
            {
                Name = createBrand.Name,
            };

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "index");
            brand.BrandLogo = await createBrand.iff_Logo.CreateImage(imageFolderPath, "brands");

            _context.Brands.Add(brand);
            _context.SaveChanges();

            return RedirectToAction(nameof(Brands));
        }

        public IActionResult Edit(int Id)
        {
            Brand brand = _context.Brands.FirstOrDefault(b => b.Id == Id);
            return View(brand);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Editing(Brand editBrand)
        {
            Brand brand = _context.Brands.FirstOrDefault(b=>b.Id == editBrand.Id);

            brand.Name = editBrand.Name;

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images", "index");

            string removePath = Path.Combine(_env.WebRootPath, "assets", "images", "index", "brands", brand.BrandLogo);

            brand.BrandLogo = await editBrand.iff_Logo.CreateImage(imageFolderPath, "brands");

            FileUpload.DeleteImage(removePath);

            _context.SaveChanges();
            return RedirectToAction(nameof(Brands));
        }

        public IActionResult Delete(int Id)
        {
            Brand brand = _context.Brands.First(b => b.Id == Id);

            return View(brand);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Deleting(Brand deleteGame)
        {
            Brand brand = _context.Brands.FirstOrDefault(b=>b.Id == deleteGame.Id);

            List<Model> models = _context.Models.Where(m=>m.BrandId == brand.Id).ToList();

            foreach (Model model in models)
            {
                _context.Models.Remove(model);
            }

            List<Car> cars = _context.Cars.Where(c => c.BrandId == brand.Id).ToList();

            foreach (Car car in cars)
            {
                _context.Cars.Remove(car);
            }

            _context.Brands.Remove(brand);

            _context.SaveChanges();

            return RedirectToAction(nameof(Brands));
        }
    }
}
