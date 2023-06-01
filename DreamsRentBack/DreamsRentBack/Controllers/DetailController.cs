using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Controllers
{
    public class DetailController : Controller
    {
        public DreamsRentDbContext _context { get; set; }
        public UserManager<User> _userManager { get; set; }

        public DetailController(DreamsRentDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region CarDetail
        public IActionResult CarDetail(int Id)
        {
            if (Id != 0)
            {
                ViewBag.Services = _context.ExtraServices.Include(es=>es.ServicesAndCars).ToList();
                ViewBag.Features = _context.CarsFeatures.Include(cf=>cf.FeaturesAndCars).ToList();
                ViewBag.Users = _context.Users.ToList();
                ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ThenInclude(c=>c.Car).ToList();
                ViewBag.Streets = _context.Streets.Include(s=>s.City).ToList();

                Car? car = _context.Cars
                    .Include(c=>c.Company).ThenInclude(c=>c.User)
                    .Include(c=>c.Company).ThenInclude(c=>c.Bookings)
                    .Include(c=>c.Company).ThenInclude(c=>c.companyPickupLocations).ThenInclude(cp => cp.PickupLocation).ThenInclude(p => p.City)
                    .Include(c=>c.Company).ThenInclude(c=>c.companyDropoffLocations).ThenInclude(cp => cp.DropoffLocation).ThenInclude(p => p.City)
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

                car.Views++;
                _context.SaveChanges();

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
                    Company = car.Company,
                    Bookings = car.Company.Bookings,
                    Availability = car.Availability,
                };

                List<Car> cars = _context.Cars.Where(c=>c.Company.Id == car.Company.Id).ToList();
                double companyRating = 0;
                int companyRatingCount = 0;

                foreach (var item in cars)
                {
                    companyRating += car.Rating;
                    companyRatingCount += car.Comments.Count() ;
                }

                ViewBag.CompanyRating = companyRating;
                ViewBag.CompanyRatingCount = companyRatingCount;

                if (car == null) { return RedirectToAction("NotFound", "Error"); }

                return View(carDetailVM);
            }
            return RedirectToAction("NotFound", "Error");
        }

        #endregion

        #region AddComment
        public async Task<IActionResult> AddComment(int Id, Comment newComment)
        {
            if (newComment.Text is null)
            {
                ModelState.AddModelError("", "Invalid review form");
                return View();
            }
            
            Car car = _context.Cars.FirstOrDefault(c => c.Id == Id);

            if (!User.Identity.IsAuthenticated)
            {
                if (newComment.Fullname is null ||  newComment.Email is null) 
                {
                    ModelState.AddModelError("Comments.FirstOrDefault(c=>c.CarId == Model.Id).Rating.Point", "Invalid review form");
                    return RedirectToAction(nameof(CarDetail), new { Id });
                }

                Rating ratingNotRegistered = new()
                {
                    Point = newComment.Rating.Point
                };

                Comment commentNotRegistered = new()
                {
                    Fullname = newComment.Fullname,
                    Email = newComment.Email,
                    Text = newComment.Text,
                    Car = car,
                    Rating = ratingNotRegistered,
                    CreationTime = DateTime.Now,
                };

                car.Rating += newComment.Rating.Point;
                car.Comments.Add(commentNotRegistered);

                await _context.Comments.AddAsync(commentNotRegistered);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CarDetail), new { Id });
            }

            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            Rating ratingRegistered = new()
            {
                Point = newComment.Rating.Point
            };

            Comment commentRegistered = new()
            {
                Text = newComment.Text,
                User = user,
                Car = car,
                Rating = ratingRegistered,
                CreationTime = DateTime.Now
            };

            double rateCount = _context.Ratings.Where(r=>r.Comment.CarId == Id).Count();

            car.Rating += newComment.Rating.Point;

            newComment.Text = string.Empty;
            //return Json(ratingRegistered.Comment);

            await _context.Comments.AddAsync(commentRegistered);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(CarDetail), new { Id });
        }
        #endregion
    }
}
