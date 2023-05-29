using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Controllers
{
    public class RentController : Controller
    {
        public DreamsRentDbContext _context;

        public RentController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout(
            int carId,
            int companyId,
            string pickLocation,
            string dropLocation,
            DateTime pickDate,
            DateTime dropDate)
        {
            Company? company = _context.Companies
                .Include(c=>c.companyPickupLocations)
                    .Include(c=>c.companyDropoffLocations)
                        .FirstOrDefault(c=>c.Id == companyId);

            if (pickLocation == null && dropLocation == null)
            {
                return RedirectToAction("Index");
            }

            string pickCity = pickLocation.Split(", ")[0];
            string pickStreet = pickLocation.Split(", ")[1];

            City? cityPick = _context.Cities.FirstOrDefault(c=>c.Name.Equals(pickCity));
            Street? streetPick = _context.Streets.FirstOrDefault(s=>s.Name.Equals(pickStreet));

            PickupLocation? pickupLocation = _context.PickupLocations
                .FirstOrDefault(p=>p.CityId == cityPick.Id && p.StreetId == streetPick.Id);

            string dropCity = dropLocation.Split(", ")[0];
            string dropStreet = dropLocation.Split(", ")[1];

            City? cityDrop = _context.Cities.FirstOrDefault(c => c.Name.Equals(dropCity));
            Street? streetDrop = _context.Streets.FirstOrDefault(s => s.Name.Equals(dropStreet));

            DropoffLocation? dropoffLocation = _context.DropoffLocations
                .FirstOrDefault(d => d.CityId == cityDrop.Id && d.StreetId == streetDrop.Id);

            if (company.companyPickupLocations.Any(cp=>cp.PickupLocationId == pickupLocation.Id) && 
                company.companyDropoffLocations.Any(cd=>cd.DropoffLocationId == dropoffLocation.Id))
            {
                Car car = _context.Cars.FirstOrDefault(c => c.Id == carId);
                return Json(pickupLocation.Id);
            }

            return View();
        }
    }
}
