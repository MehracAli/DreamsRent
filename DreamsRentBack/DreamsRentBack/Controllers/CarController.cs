using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities.Extentions;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DreamsRentBack.Controllers
{
    public class CarController : Controller
    {
        public DreamsRentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public CarController(DreamsRentDbContext context, UserManager<User> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        #region CreateCar
        public IActionResult AddCar()
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Bodies = _context.Bodys.ToList();
            ViewBag.FuelTypes = _context.FuelTypes.ToList();
            ViewBag.Engines = _context.Engines.ToList();
            ViewBag.Brakes = _context.Brakes.ToList();
            ViewBag.Transmissions = _context.Transmissions.ToList();
            ViewBag.Drivetrians = _context.Drivetrians.ToList();
            ViewBag.AirConditions = _context.AirConditions.ToList();
            ViewBag.Services = _context.ExtraServices.ToList();
            ViewBag.Features = _context.CarsFeatures.ToList();

            return View();
        }
        //AddCreate Start
        [HttpPost]
        [ActionName(nameof(AddCar))]
        public async Task<IActionResult> AddCar(CreateCarVM createdCar)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Bodies = _context.Bodys.ToList();
            ViewBag.FuelTypes = _context.FuelTypes.ToList();
            ViewBag.Engines = _context.Engines.ToList();
            ViewBag.Brakes = _context.Brakes.ToList();
            ViewBag.Transmissions = _context.Transmissions.ToList();
            ViewBag.Drivetrians = _context.Drivetrians.ToList();
            ViewBag.AirConditions = _context.AirConditions.ToList();
            ViewBag.Services = _context.ExtraServices.ToList();
            ViewBag.Features = _context.CarsFeatures.ToList();
            //return Json(createdCar);
            //if (createdCar == null){ return View(); }

            

            if (!ModelState.IsValid)
            {
                CheckModelState(createdCar.BrandId, "BrandId", "Select brand");
                CheckModelState(createdCar.ModelId, "ModelId", "Select model");
                CheckModelState(createdCar.BodyId, "BodyId", "Select vehicles type");
                CheckModelState(createdCar.FuelTypeId, "FuelTypeId", "Select fuel type");
                CheckModelState(createdCar.EngineId, "EngineId", "Select engine power");
                CheckModelState(createdCar.BrakeId, "BrakeId", "Select brake type");
                CheckModelState(createdCar.TransmissionId, "TransmissionId", "Select transmission type");
                CheckModelState(createdCar.DrivertrianId, "DrivertrianId", "Select drivetrian type");
                CheckModelState(createdCar.AirConditionId, "AirConditionId", "Select air condition type");

                if (!createdCar.iff_CarPhotos.Any(cp => cp.IsValidFile("image/")))
                {
                    ModelState.AddModelError("iff_CarPhotos", "Please choose image as: jpg, png...");
                    return View();
                }
                if (!createdCar.iff_CarPhotos.Any(cp => cp.IsValidLength(3)))
                {
                    ModelState.AddModelError("iff_CarPhotos", "Please choose image which size is maximum 2MB");
                    return View();
                }

                return View(createdCar);
            }

            User user = await _userManager.GetUserAsync(HttpContext.User);
            Company company = _context.Companies.FirstOrDefault(c => c.UserId == user.Id);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Other options as needed
            };

            string json = JsonSerializer.Serialize(company, options);

            //return Content(json, "application/json");

            Car car = new()
            {
                CompanyId = company.Id,
                BrandId = createdCar.BrandId,
                ModelId = createdCar.ModelId,
                Year = createdCar.Year,
                BodyId = createdCar.BodyId,
                FuelTypeId = createdCar.FuelTypeId,
                Speed = createdCar.Speed,
                EngineId = createdCar.EngineId,
                BrakeId = createdCar.BrakeId,
                Door = createdCar.Door,
                Capacity = createdCar.Capacity,
                TransmissionId = createdCar.TransmissionId,
                DrivetrianId = createdCar.DrivertrianId,
                AirConditionId = createdCar.AirConditionId,
                VIN = createdCar.VIN,
                Price = createdCar.Price,
                Description = createdCar.Description,
                Availability = true,
                Views = 0,
                Rating = 0,
            };

            string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");

            foreach (IFormFile image in createdCar.iff_CarPhotos)
            {
                if (!image.IsValidFile("image/") || !image.IsValidLength(2))
                {
                    TempData["InvalidImages"] += image.FileName;
                    continue;
                }

                CarPhoto newImage = new()
                {
                    Path = await image.CreateImage(imageFolderPath, "cars")
                };

                car.CarPhotos.Add(newImage);
            }

            foreach (int id in createdCar.ServicesIds)
            {
                ExtraServicesAndCars servicesAndCars = new()
                {
                    ExtraServiceId = id
                };
                car.ServicesAndCars.Add(servicesAndCars);
            }

            foreach (int id in createdCar.FeaturesIds)
            {
                CarFeaturesAndCars carFeaturesAndCars = new()
                {
                    CarFeaturesId = id
                };
                car.FeaturesAndCars.Add(carFeaturesAndCars);
            }

            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToAction("CompanyAccount", "Account");
        }
        //AddCreate End

        public void CheckModelState(int id, string model, string modelError)
        {
            if(id != null && model != null && modelError != null)
            {
                if (id == 0) { ModelState.AddModelError(model, modelError); }
            }
        }

        public IActionResult GetBrandModels(int brandId)
        {
            List<Model> models = _context.Models.Where(m => m.BrandId.Equals(brandId)).ToList();

            if (models == null)
            {
                return Json(new { status = 404 });
            }

            return Json(models);
        }
        #endregion

        
    }
}
