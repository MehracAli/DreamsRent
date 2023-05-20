using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.ViewModels.CarViewModels;
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
                ViewBag.Services = _context.ExtraServices.Include(es=>es.ServicesAndCars).ToList();
                ViewBag.Features = _context.CarsFeatures.Include(cf=>cf.FeaturesAndCars).ToList();
                ViewBag.Users = _context.Users.ToList();

                Car? car = _context.Cars
                    .Include(c=>c.Company)
                    .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                        .Include(c=>c.Body)
                        .Include(c=>c.Transmission)
                            .Include(c=>c.FuelType)
                            .Include(c=>c.CarPhotos)
                                .Include(c=>c.ServicesAndCars).ThenInclude(sc=>sc.ExtraService)
                                .Include(c=>c.FeaturesAndCars).ThenInclude(fc=>fc.CarFeatures)
                                    .Include(c=>c.Drivetrian)
                                    .Include(c=>c.AirCondition)
                                        .Include(c=>c.Brake)
                                        .Include(c=>c.Engine)
                                        .Include(c=>c.Comments).ThenInclude(c=>c.Rating)
                                            .FirstOrDefault(c => c.Id == Id);

                CarDetailVM carDetailVM = new()
                {
                    Id = car.Id,
                    Rating = car.Rating,
                    Brand = car.Brand,
                    ModelId = car.ModelId,
                    Location = car.Company.Location,
                    Views = car.Views,
                    CarPhotos = car.CarPhotos,
                    ServicesAndCars = car.ServicesAndCars,
                    Body = car.Body,
                    Transmission = car.Transmission,
                    FuelType = car.FuelType,
                    Speed = car.Speed,
                    Drivetrian = car.Drivetrian,
                    Year = car.Year,
                    AirCondition = car.AirCondition,
                    VIN = car.VIN,
                    Door = car.Door,
                    Brake = car.Brake,
                    Engine = car.Engine,
                    FeaturesAndCars = car.FeaturesAndCars,
                    Comments = car.Comments,
                    Description = car.Description,
                };

                if (car == null) { return RedirectToAction("NotFound", "Error"); }

                return View(carDetailVM);
            }
            return RedirectToAction("NotFound", "Error");
        }
        #endregion
    }
}
