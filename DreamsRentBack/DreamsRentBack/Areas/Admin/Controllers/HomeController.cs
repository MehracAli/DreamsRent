using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Areas.Admin.Controllers
{
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
