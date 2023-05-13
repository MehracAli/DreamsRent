using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
