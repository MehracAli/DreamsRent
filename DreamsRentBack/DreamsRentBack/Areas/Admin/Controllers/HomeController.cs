using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        public DreamsRentDbContext _context;
        public HomeController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.ContactUsMessages = _context.ContactUsMessages.OrderByDescending(c=>c.Id).ToList();
            ViewBag.Cars = _context.Cars
                .Include(c=>c.Company)
                .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                    .OrderByDescending(c=>c.Id)
                    .ToList();
            ViewBag.Companies = _context.Companies.ToList();
            ViewBag.Consumer = _context.Users.Include(u=>u.Company).ToList();
            ViewBag.Bookings = _context.Bookings.ToList();
            List<Brand> brands = _context.Brands.Include(b=>b.Cars).ToList();
            List<Company> companies = _context.Companies.Include(c=>c.Bookings).ToList();

            List<string> brandNames = new();
            List<int> carCount = new();
            List<string> companyNames = new();
            List<int> bookingCount = new();

            foreach (var item in brands)
            {
                brandNames.Add(item.Name);
                carCount.Add(item.Cars.Count());
            }
            foreach (var item in companies)
            {
                companyNames.Add(item.CompanyName);
                bookingCount.Add(item.Bookings.Count());
            }

            ViewBag.BrandNames = brandNames;
            ViewBag.CarCount = carCount;
            ViewBag.CompanyNames = companyNames;
            ViewBag.BookingCount = bookingCount;

            return View();
        }

        public IActionResult VeriifyCompany(int CompanyId)
        {
            Company company = _context.Companies.FirstOrDefault(c=>c.Id == CompanyId);

            company.Verification = true;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
