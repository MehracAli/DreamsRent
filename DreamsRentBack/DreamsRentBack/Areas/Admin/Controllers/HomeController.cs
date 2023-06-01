using DreamsRentBack.DAL;
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
            return View();
        }
    }
}
