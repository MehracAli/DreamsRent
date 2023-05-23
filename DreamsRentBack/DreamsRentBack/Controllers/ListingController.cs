using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.ViewModels.CarViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace DreamsRentBack.Controllers
{
    public class Listingcontroller : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public Listingcontroller(DreamsRentDbContext context)
        {
            _context = context;
        }

        public ActionResult Index() 
        {
            ViewBag.Brands = _context.Brands.Include(b=>b.Models).OrderBy(b=>b.Name).ToList();
            ViewBag.Bodies = _context.Bodys.OrderBy(b=>b.Name).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();
            List<Car> expensives = _context.Cars.OrderBy(c=>c.Price).ToList();
            ViewBag.Expensive = expensives.First().Price;


            List<CarExploreVM> cars = _context.Cars
                .Include(c => c.CarPhotos)
                    .Include(c => c.Likes)
                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                            .Include(c => c.Company)
                                .OrderByDescending(c => c.Id)
            .Select(c=> new CarExploreVM
            {
                Id = c.Id,
                CarPhotos = c.CarPhotos,
                Likes = c.Likes,
                Brand = c.Brand,
                ModelId = c.ModelId,
                Price = c.Price,
                Rating = c.Rating,
                Transmission = c.Transmission,
                Speed = c.Speed,
                FuelType = c.FuelType,
                Engine = c.Engine,
                Year = c.Year,
                Capacity = c.Capacity,
                Company = c.Company,
            })
            .Take(6)
            .ToList();

            return View(cars);
        }

        [HttpPost]
        public IActionResult Index(int[] selectedBrands, int[] selectedBodies = null, int selectedCapacity = 0, int selectedPrice = 0, int selectedRating = 0)
        {
            ViewBag.Brands = _context.Brands.Include(b => b.Models).OrderBy(b => b.Name).ToList();
            ViewBag.Bodies = _context.Bodys.OrderBy(b => b.Name).ToList();
            ViewBag.Ratings = _context.Ratings.Include(r => r.Comment).ToList();
            List<Car> expensives = _context.Cars.OrderBy(c => c.Price).ToList();
            ViewBag.Expensive = expensives.First().Price;
            List<Car> currentCars = _context.Cars
                .Include(c => c.CarPhotos)
                                    .Include(c => c.Likes)
                                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                                        .Include(c => c.Company)
                                            .Include(c => c.Transmission)
                                            .Include(c => c.FuelType)
                                                .Include(c => c.Engine)
                                                 .OrderByDescending(c => c.Id)
                                                    .Take(6).ToList();

            if (selectedBrands.Count() == 0 && selectedBodies.Count() == 0 && selectedCapacity == 0 && selectedPrice == 0 && selectedRating == 0)
            {
                List<CarExploreVM> currentCarExploreVMs = currentCars.Distinct().Select(c => new CarExploreVM()
                {
                    Id = c.Id,
                    CarPhotos = c.CarPhotos,
                    Likes = c.Likes,
                    Brand = c.Brand,
                    ModelId = c.ModelId,
                    Price = c.Price,
                    Rating = c.Rating,
                    Transmission = c.Transmission,
                    Speed = c.Speed,
                    FuelType = c.FuelType,
                    Engine = c.Engine,
                    Year = c.Year,
                    Capacity = c.Capacity,
                    Company = c.Company,

                })
                .OrderByDescending(c => c.Id)
                .Take(6).ToList();

                return View(currentCarExploreVMs);
            }

            List<Body> getBodies = new();
            List<Car> cars = new();

            if (selectedBrands.Count() != 0)
            {
                foreach (int brandId in selectedBrands)
                {
                    List<Car>? findById  = _context.Cars
                        .Include(c => c.CarPhotos)
                                    .Include(c => c.Likes)
                                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                                        .Include(c => c.Company)
                                            .Include(c => c.Transmission)
                                            .Include(c => c.FuelType)
                                                .Include(c => c.Engine)
                                                .OrderByDescending(c => c.Id)
                                                    .Where(c => c.BrandId == brandId).ToList();
                    foreach (Car car in findById)
                    {
                        cars.Add(car);
                    }
                }
            }
            //-------
            if (selectedBodies.Count() != 0)
            {
                foreach (int bodyId in selectedBodies)
                {
                    if(selectedBrands.Count() == 0)
                    {
                        List<Car> findByBodyId2 = _context.Cars
                             .Include(c => c.CarPhotos)
                                    .Include(c => c.Likes)
                                        .Include(c => c.Brand).ThenInclude(b => b.Models)
                                        .Include(c => c.Company)
                                            .Include(c => c.Transmission)
                                            .Include(c => c.FuelType)
                                                .Include(c => c.Engine)
                                                .OrderByDescending(c => c.Id)
                                                    .Where(c=>c.BodyId == bodyId)
                                                    .ToList();

                        foreach (Car car in findByBodyId2)
                        {
                            cars.Add(car);
                        }
                    }

                    List<Car> findByBodyId = cars.Where(c => c.BodyId == bodyId).ToList();

                    cars = findByBodyId;

                }
            }
            //-------
            if (selectedCapacity != 0)
            {
                if (selectedBrands.Count() == 0 && selectedBodies.Count() == 0)
                {
                    if (selectedCapacity == 1) 
                    {
                        List<Car> findByCapacity = _context.Cars
                            .Include(c => c.CarPhotos)
                                        .Include(c => c.Likes)
                                            .Include(c => c.Brand).ThenInclude(b => b.Models)
                                            .Include(c => c.Company)
                                                .Include(c => c.Transmission)
                                                .Include(c => c.FuelType)
                                                    .Include(c => c.Engine)
                                                    .OrderByDescending(c => c.Id)
                                                        .Where(c=>c.Capacity >= 2 && c.Capacity <= 4)
                                                        .ToList();

                        foreach(Car car in findByCapacity)
                        {
                            cars.Add(car);
                        }
                    }
                    if (selectedCapacity == 2)
                    {
                        List<Car> findByCapacity = _context.Cars
                            .Include(c => c.CarPhotos)
                                        .Include(c => c.Likes)
                                            .Include(c => c.Brand).ThenInclude(b => b.Models)
                                            .Include(c => c.Company)
                                                .Include(c => c.Transmission)
                                                .Include(c => c.FuelType)
                                                    .Include(c => c.Engine)
                                                    .OrderByDescending(c => c.Id)
                                                        .Where(c => c.Capacity >= 4 && c.Capacity <= 6)
                                                        .ToList();

                        foreach (Car car in findByCapacity)
                        {
                            cars.Add(car);
                        }
                    }
                    if (selectedCapacity == 3)
                {
                    List<Car> findByCapacity = _context.Cars
                       .Include(c => c.CarPhotos)
                                   .Include(c => c.Likes)
                                       .Include(c => c.Brand).ThenInclude(b => b.Models)
                                       .Include(c => c.Company)
                                           .Include(c => c.Transmission)
                                           .Include(c => c.FuelType)
                                               .Include(c => c.Engine)
                                               .OrderByDescending(c => c.Id)
                                                   .Where(c => c.Capacity > 6 && c.Capacity < 10)
                                                   .ToList();

                    foreach (Car car in findByCapacity)
                    {
                        cars.Add(car);
                    }
                }
                }
                if (selectedBrands.Count() != 0) 
                {
                    if (selectedCapacity == 1)
                    {
                        List<Car> findByCapacity = cars.Where(c => c.Capacity >= 2 && c.Capacity <= 4).ToList();
                        cars = findByCapacity.ToList();
                    }
                    if (selectedCapacity == 2)
                    {
                        List<Car> findByCapacity = cars.Where(c => c.Capacity >= 4 && c.Capacity <= 6).ToList();
                        cars = findByCapacity.ToList();
                    }
                    if (selectedCapacity == 3)
                    {
                        List<Car> findByCapacity = cars.Where(c => c.Capacity > 6 && c.Capacity < 10).ToList();
                        cars = findByCapacity.ToList();
                    }
                }
                if (selectedBodies.Count() != 0)
                {
                    if (selectedCapacity == 1)
                    {
                        List<Car> findByCapacity = cars.Where(c => c.Capacity >= 2 && c.Capacity <= 4).ToList();
                        cars = findByCapacity.ToList();
                    }
                    if (selectedCapacity == 2)
                    {
                        List<Car> findByCapacity = cars.Where(c => c.Capacity >= 4 && c.Capacity <= 6).ToList();
                        cars = findByCapacity.ToList();
                    }
                    if (selectedCapacity == 3)
                    {
                        List<Car> findByCapacity = cars.Where(c => c.Capacity > 6 && c.Capacity < 10).ToList();
                        cars = findByCapacity.ToList();
                    }
                }
            }
            //-------
            if (selectedPrice != 0 )
            {
                if (selectedBrands.Count() == 0 && selectedBodies.Count() == 0 && selectedCapacity == 0) 
                {
                    if (selectedPrice == 1)
                    {
                        List<Car> findByPrice = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .Where(c => c.Price > 0 && c.Price <= 50)
                                                       .ToList();

                        //return Json(findByPrice.First().Id);

                        foreach (Car car in findByPrice)
                        {
                            cars.Add(car);
                        }
                    }

                    if (selectedPrice == 2)
                    {
                        List<Car> findByPrice = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .Where(c => c.Price >= 50 && c.Price <= 150)
                                                       .ToList();

                        foreach (Car car in findByPrice)
                        {
                            cars.Add(car);
                        }
                    }

                    if (selectedPrice == 3)
                    {
                        List<Car> findByPrice = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .Where(c => c.Price >= 150 && c.Price <= 300)
                                                       .ToList();

                        foreach (Car car in findByPrice)
                        {
                            cars.Add(car);
                        }
                    }

                    if (selectedPrice == 4)
                    {
                        List<Car> findByPrice = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .Where(c => c.Price >= 300 && c.Price <= 500)
                                                       .ToList();

                        foreach (Car car in findByPrice)
                        {
                            cars.Add(car);
                        }
                    }

                    if (selectedPrice == 5)
                    {
                        List<Car> findByPrice = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .Where(c => c.Price >= 500 && c.Price <= 1000)
                                                       .ToList();

                        foreach (Car car in findByPrice)
                        {
                            cars.Add(car);
                        }
                    }

                    if (selectedPrice == 6)
                {
                    List<Car> findByPrice = _context.Cars
                       .Include(c => c.CarPhotos)
                                   .Include(c => c.Likes)
                                       .Include(c => c.Brand).ThenInclude(b => b.Models)
                                       .Include(c => c.Company)
                                           .Include(c => c.Transmission)
                                           .Include(c => c.FuelType)
                                               .Include(c => c.Engine)
                                               .OrderByDescending(c => c.Id)
                                                   .Where(c => c.Price >= 1000)
                                                   .ToList();

                    foreach (Car car in findByPrice)
                    {
                        cars.Add(car);
                    }
                }
                }
                if (selectedBrands.Count() != 0)
                {
                    if (selectedPrice == 1)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price > 0 && c.Price <= 50)
                                    .ToList();
                        cars = findByPrice;
                    }

                    if (selectedPrice == 2)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 50 && c.Price <= 150)
                                    .ToList();
                        cars = findByPrice;
                    }

                    if (selectedPrice == 3)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 150 && c.Price <= 300)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 4)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 300 && c.Price <= 500)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 5)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 500 && c.Price <= 1000)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 6)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 1000)
                                    .ToList();
                            
                        cars = findByPrice;
                    }
                }
                if (selectedBodies.Count() != 0)
                {
                    if (selectedPrice == 1)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price > 0 && c.Price <= 50)
                                    .ToList();
                        cars = findByPrice;
                    }

                    if (selectedPrice == 2)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 50 && c.Price <= 150)
                                    .ToList();
                        cars = findByPrice;
                    }

                    if (selectedPrice == 3)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 150 && c.Price <= 300)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 4)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 300 && c.Price <= 500)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 5)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 500 && c.Price <= 1000)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 6)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 1000)
                                    .ToList();

                        cars = findByPrice;
                    }
                }
                if (selectedCapacity != 0)
                {
                    if (selectedPrice == 1)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price > 0 && c.Price <= 50)
                                    .ToList();
                        cars = findByPrice;
                    }

                    if (selectedPrice == 2)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 50 && c.Price <= 150)
                                    .ToList();
                        cars = findByPrice;
                    }

                    if (selectedPrice == 3)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 150 && c.Price <= 300)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 4)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 300 && c.Price <= 500)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 5)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 500 && c.Price <= 1000)
                                    .ToList();

                        cars = findByPrice;
                    }

                    if (selectedPrice == 6)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => c.Price >= 1000)
                                    .ToList();

                        cars = findByPrice;
                    }
                }
            }
            //-------
            if (selectedRating != 0)
            {
                List<Rating> ratings = _context.Ratings.Include(r=>r.Comment).ToList();

                if (selectedBrands.Count() == 0 && selectedBodies.Count() == 0 && selectedCapacity == 0 && selectedPrice == 0)
                {
                    if (selectedRating == 1)
                    {
                        List<Car> findByRating = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .ToList();

                        foreach (Car car in findByRating)
                        {
                            double rate = car.Rating / ratings.FindAll(r => r.Comment.CarId == car.Id).Count();
                            if (rate >= 1 && rate < 2)
                            {
                                cars.Add(car);
                            }
                        }
                    }

                    if (selectedRating == 2)
                    {
                        List<Car> findByRating = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .ToList();

                        foreach (Car car in findByRating)
                        {
                            double rate = car.Rating / ratings.FindAll(r => r.Comment.CarId == car.Id).Count();
                            if (rate >= 2 && rate < 3)
                            {
                                cars.Add(car);
                            }
                        }
                    }

                    if (selectedRating == 3)
                    {
                        List<Car> findByRating = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .ToList();

                        foreach (Car car in findByRating)
                        {
                            double rate = car.Rating / ratings.FindAll(r => r.Comment.CarId == car.Id).Count();
                            if (rate >= 3 && rate < 4)
                            {
                                cars.Add(car);
                            }
                        }
                    }

                    if (selectedRating == 4)
                    {
                        List<Car> findByRating = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .ToList();

                        foreach (Car car in findByRating)
                        {
                            double rate = car.Rating / ratings.FindAll(r => r.Comment.CarId == car.Id).Count();
                            if (rate>=4 && rate < 5)
                            {
                                cars.Add(car);
                            }
                        }
                    }

                    if (selectedRating == 5)
                    {
                        List<Car> findByRating = _context.Cars
                           .Include(c => c.CarPhotos)
                                       .Include(c => c.Likes)
                                           .Include(c => c.Brand).ThenInclude(b => b.Models)
                                           .Include(c => c.Company)
                                               .Include(c => c.Transmission)
                                               .Include(c => c.FuelType)
                                                   .Include(c => c.Engine)
                                                   .OrderByDescending(c => c.Id)
                                                       .ToList();

                        foreach (Car car in findByRating)
                        {
                            double rate = car.Rating / ratings.FindAll(r => r.Comment.CarId == car.Id).Count();
                            if (rate == 5)
                            {
                                cars.Add(car);
                            }
                        }
                    }
                }
                if (selectedBrands.Count() != 0)
                {
                    if (selectedRating == 1)
                    {
                        double rate = 0;

                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c=>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 1 && 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) < 2)
                                                .ToList();

                        cars = findByRating;
                        
                    }

                    if (selectedRating == 2)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 2 && 
                                ((double)c.Rating / ratings
                                    .Where(r => r.Comment.CarId == c.Id)
                                        .Count()) < 3)
                                            .ToList();

                        cars = findByPrice;


                    }

                    if (selectedRating == 3)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 3 && 
                                     ((double)c.Rating / ratings
                                         .Where(r => r.Comment.CarId == c.Id)
                                             .Count()) < 4)
                                                .ToList();

                        cars = findByPrice;
                    }

                    if (selectedRating == 4)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 4 && 
                                ((double)c.Rating / ratings
                                    .Where(r => r.Comment.CarId == c.Id)
                                        .Count()) < 5)
                                            .ToList();

                        cars = findByPrice;
                    }

                    if (selectedRating == 5)
                    {
                        List<Car> findByPrice = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) == 5)
                                                .ToList();

                        cars = findByPrice;
                    }
                }
                if (selectedBodies.Count() != 0)
                {
                    if (selectedRating == 1)
                    {
                        double rate = 0;

                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 1 && 
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) < 2)
                                                .ToList();

                        cars = findByRating;

                    }

                    if (selectedRating == 2)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => ((double)c.Rating / ratings.Where(r => r.Comment.CarId == c.Id).Count()) >= 2 && ((double)c.Rating / ratings.Where(r => r.Comment.CarId == c.Id).Count()) < 3)
                                    .ToList();

                        cars = findByRating;


                    }

                    if (selectedRating == 3)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 3 &&
                                     ((double)c.Rating / ratings
                                         .Where(r => r.Comment.CarId == c.Id)
                                             .Count()) < 4)
                                                .ToList();

                        cars = findByRating;
                    }

                    if (selectedRating == 4)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 4 &&
                                ((double)c.Rating / ratings
                                    .Where(r => r.Comment.CarId == c.Id)
                                        .Count()) < 5)
                                            .ToList();

                        cars = findByRating;
                    }

                    if (selectedRating == 5)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) == 5)
                                                .ToList();

                        cars = findByRating;
                    }
                }
                if (selectedCapacity != 0)
                {
                    if (selectedRating == 1)
                    {
                        double rate = 0;

                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 1 &&
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) < 2)
                                                .ToList();

                        cars = findByRating;

                    }

                    if (selectedRating == 2)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => ((double)c.Rating / ratings.Where(r => r.Comment.CarId == c.Id).Count()) >= 2 && ((double)c.Rating / ratings.Where(r => r.Comment.CarId == c.Id).Count()) < 3)
                                    .ToList();

                        cars = findByRating;


                    }

                    if (selectedRating == 3)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 3 &&
                                     ((double)c.Rating / ratings
                                         .Where(r => r.Comment.CarId == c.Id)
                                             .Count()) < 4)
                                                .ToList();

                        cars = findByRating;
                    }

                    if (selectedRating == 4)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 4 &&
                                ((double)c.Rating / ratings
                                    .Where(r => r.Comment.CarId == c.Id)
                                        .Count()) < 5)
                                            .ToList();

                        cars = findByRating;
                    }

                    if (selectedRating == 5)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) == 5)
                                                .ToList();

                        cars = findByRating;
                    }
                }
                if (selectedPrice != 0)
                {
                    if (selectedRating == 1)
                    {
                        double rate = 0;

                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 1 &&
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) < 2)
                                                .ToList();

                        cars = findByRating;

                    }

                    if (selectedRating == 2)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c => ((double)c.Rating / ratings.Where(r => r.Comment.CarId == c.Id).Count()) >= 2 && ((double)c.Rating / ratings.Where(r => r.Comment.CarId == c.Id).Count()) < 3)
                                    .ToList();

                        cars = findByRating;


                    }

                    if (selectedRating == 3)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 3 &&
                                     ((double)c.Rating / ratings
                                         .Where(r => r.Comment.CarId == c.Id)
                                             .Count()) < 4)
                                                .ToList();

                        cars = findByRating;
                    }

                    if (selectedRating == 4)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) >= 4 &&
                                ((double)c.Rating / ratings
                                    .Where(r => r.Comment.CarId == c.Id)
                                        .Count()) < 5)
                                            .ToList();

                        cars = findByRating;
                    }

                    if (selectedRating == 5)
                    {
                        List<Car> findByRating = cars
                            .OrderByDescending(c => c.Id)
                                .Where(c =>
                                    ((double)c.Rating / ratings
                                        .Where(r => r.Comment.CarId == c.Id)
                                            .Count()) == 5)
                                                .ToList();

                        cars = findByRating;
                    }
                }
            }

            List<CarExploreVM> carExploreVMs = cars.Distinct().Select(c => new CarExploreVM()
            {
                Id = c.Id,
                CarPhotos = c.CarPhotos,
                Likes = c.Likes,
                Brand = c.Brand,
                ModelId = c.ModelId,
                Price = c.Price,
                Rating = c.Rating,
                Transmission = c.Transmission,
                Speed = c.Speed,
                FuelType = c.FuelType,
                Engine = c.Engine,
                Year = c.Year,
                Capacity = c.Capacity,
                Company = c.Company,

            })
            .OrderByDescending(c => c.Id)
            .Take(6).ToList();
            
            return View(carExploreVMs);      
        }
    }
}
