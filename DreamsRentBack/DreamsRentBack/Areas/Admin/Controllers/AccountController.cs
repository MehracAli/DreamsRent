using DreamsRentBack.Areas.Admin.ViewModels;
using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public DreamsRentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(DreamsRentDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(AdminLoginVM loginRequestUser)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "User name or password was incorrect");
                return View();
            }

            User user = await _userManager.FindByNameAsync(loginRequestUser.UserName);

            if (user is null)
            {
                ModelState.AddModelError("", "User name or password was incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager
                    .PasswordSignInAsync(
                        user,
                        loginRequestUser.Password,
                        loginRequestUser.RememberMe,
                        true
                    );

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "You can try to login after 5 minutes");
                    return View();
                }
                ModelState.AddModelError("", "Incorrect Username or Password");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
