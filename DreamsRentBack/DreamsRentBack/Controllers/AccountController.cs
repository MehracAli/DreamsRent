using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities;
using DreamsRentBack.ViewModels.Identify;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;

namespace DreamsRentBack.Controllers
{
    public class AccountController : Controller
    {
        public DreamsRentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(DreamsRentDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region SignupLogin
            //CONSUMER SIGNUP START
            public IActionResult ConsumerSignup()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> ConsumerSignup(ConsumerRegisterVM registerRequestConsumer)
            {
                if(!ModelState.IsValid) { return View(); }
                if (!registerRequestConsumer.Terms) { return View(); }
                if (_context.Users.Any(u => u.Email.Equals(registerRequestConsumer.Email)))
                {
                    ModelState.AddModelError("Email", "This email already exists");

                    return View();
                }

                User user = new()
                {
                    Name = registerRequestConsumer.Name,
                    Surname = registerRequestConsumer.Surname,
                    UserName = registerRequestConsumer.UserName,
                    Email = registerRequestConsumer.Email,
                    PhoneNumber = registerRequestConsumer.PhoneNumber,
                    IsCompany = false
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerRequestConsumer.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action(
                        nameof(ConfirmEmail),
                        "Account",
                        new
                        {
                            user.Email,
                            token
                        },
                        Request.Scheme, 
                        Request.Host.ToString()
                    );

                MailMessage message = new MailMessage();
                message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
                message.To.Add(new MailAddress(user.Email));
                message.Subject = "Verify Email";
                message.Body = string.Empty;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader("wwwroot/confirmation.html"))
                {
                    body = reader.ReadToEnd();
                }
                message.Body = body.Replace("{{link}}", url);
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential("dreamsrentofficial@gmail.com", "cxpstlrytkzgyrdk");
                smtpClient.Send(message);

                await _userManager.AddToRoleAsync(user, "Consumer");
                return RedirectToAction("Index", "Home");
            }
            //CONSUMER SIGNUP END

            //COMPANY SIGNUP START
            public IActionResult CompanySignup() 
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> CompanySignup(CompanyRegisterVM registerRequestCompany)
            {
                if (!ModelState.IsValid) { return View(); }
                if (!registerRequestCompany.Terms) { return View(); }
                if (_context.Users.Any(u => u.Email.Equals(registerRequestCompany.Email)))
                {
                    ModelState.AddModelError("Email", "This email already exists");

                    return View();
                }

                Company company = new();
                company.CompanyName = registerRequestCompany.CompanyName;

                User user = new()
                {
                    UserName = registerRequestCompany.UserName,
                    Email = registerRequestCompany.Email,
                    PhoneNumber = registerRequestCompany.PhoneNumber,
                    IsCompany = true,
                    Company = company
                };

                company.UserId = user.Id;

                IdentityResult result = await _userManager.CreateAsync(user, registerRequestCompany.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action(
                        nameof(ConfirmEmail),
                        "Account",
                        new
                        {
                            user.Email,
                            token
                        },
                        Request.Scheme,
                        Request.Host.ToString()
                    );

                MailMessage message = new MailMessage();
                message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
                message.To.Add(new MailAddress(user.Email));
                message.Subject = "Verify Email";
                message.Body = string.Empty;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader("wwwroot/confirmation.html"))
                {
                    body = reader.ReadToEnd();
                }
                message.Body = body.Replace("{{link}}", url);
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new NetworkCredential("dreamsrentofficial@gmail.com", "cxpstlrytkzgyrdk");
                smtpClient.Send(message);

                await _userManager.AddToRoleAsync(user, "Company");
                return RedirectToAction("Index", "Home");
            }
            //COMPANY SIGNUP END

            //SIGNUP OTHER START
            public async Task<IActionResult> ConfirmEmail(string token, string email)
            {
                User user = await _userManager.FindByEmailAsync(email);

                if (user == null) return BadRequest("User not registered");

                await _userManager.ConfirmEmailAsync(user, token);
                await _signInManager.SignInAsync(user, true);

                return RedirectToAction("Index", "Home");
            }
            public IActionResult Privacy()
            {
                return View();
            }
            //SIGNUP OTHER END


            //SIGNIN START
            public IActionResult Signin()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Signin(LoginVM loginRequestUser)
            {
                if (!ModelState.IsValid) return View();

                User user = await _userManager.FindByNameAsync(loginRequestUser.UserName);

                if (user is null)
                {
                    ModelState.AddModelError("", "Incorrect Username or Password");
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
            //SIGNIN END

            //SIGNOUT START
            public async Task<IActionResult> Signout()
            {
                _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
        //SIGNOUT END
        #endregion

        #region MyAccount
            
        public IActionResult MyAccount()
        {
            return View();
        }

        #endregion


        //public async Task AddRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole(AccountRoles.Admin.ToString()));
        //    await _roleManager.CreateAsync(new IdentityRole(AccountRoles.Consumer.ToString()));
        //    await _roleManager.CreateAsync(new IdentityRole(AccountRoles.Company.ToString()));
        //}
    }
}
