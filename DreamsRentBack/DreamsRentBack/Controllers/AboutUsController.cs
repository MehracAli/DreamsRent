using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Controllers
{
    public class AboutUsController : Controller
    {
        public DreamsRentDbContext _context;

        public AboutUsController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Privacy() 
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string name, string email, string phoneNumber, string comment)
        {
            ContactUs contactUs = new()
            {
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Comment = comment,
                SendTime = DateTime.Now,
            };

            _context.ContactUsMessages.Add(contactUs);
            _context.SaveChanges();
            return View();
        }
    }
}
