using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
