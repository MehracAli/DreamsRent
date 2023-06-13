using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities;
using DreamsRentBack.Utilities.Extentions;
using DreamsRentBack.ViewModels.Account;
using DreamsRentBack.ViewModels.Identify;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DreamsRentBack.Migrations;
using System.Security.Claims;

namespace DreamsRentBack.Controllers
{
    public class AccountController : Controller
    {
        public DreamsRentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(DreamsRentDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }

        #region SignupLogin
            public IActionResult ConsumerSignup()
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
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
                    UserPhoto = "Default_pfp.svg.png",
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

            public IActionResult SigninGoogle(string url = null)
            {
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback", "Account", new { url }));
                return Challenge(properties, "Google");
            }
        
            public async Task<IActionResult> GoogleCallback(string url = null)
            {
                var signinDetails = await _signInManager.GetExternalLoginInfoAsync();
                if (signinDetails == null)
                {
                    return RedirectToAction("ConsumerSignup");
                }

                var email = signinDetails.Principal.FindFirstValue(ClaimTypes.Email);
                var name = signinDetails.Principal.FindFirstValue(ClaimTypes.Name);
                var surname = signinDetails.Principal.FindFirstValue(ClaimTypes.Surname);

                var isUserHave = await _userManager.FindByEmailAsync(email);

                if (isUserHave != null)
                {
                    await _signInManager.SignInAsync(isUserHave, isPersistent: false);

                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }

                    return RedirectToAction("Index", "Home");
                }

                var newUser = new User
                {
                    Name = name,
                    Surname = surname,
                    UserName = name.ToLower()+""+surname.ToLower(),
                    Email = email,
                    EmailConfirmed = true

                };

                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, AccountRoles.Consumer.ToString());

                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("ConsumerSignup");
                }
            }

            public IActionResult SigninFacebook(string url = null)
            {
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback", "Account", new { url }));
                return Challenge(properties, "Facebook");
            }

            public async Task<IActionResult> FacebookCallback(string url = null)
            {
                var SigninDetails = await _signInManager.GetExternalLoginInfoAsync();
                if (SigninDetails == null)
                {
                    return RedirectToAction("Register");
                }

                var email = SigninDetails.Principal.FindFirstValue(ClaimTypes.Email);
                var name = SigninDetails.Principal.FindFirstValue(ClaimTypes.Name);
                var surname = SigninDetails.Principal.FindFirstValue(ClaimTypes.Surname);

                var isUserHave = await _userManager.FindByEmailAsync(email);
                if (isUserHave != null)
                {
                    await _signInManager.SignInAsync(isUserHave, isPersistent: false);

                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }

                    return RedirectToAction("Index", "Home");
                }

                var newUser = new User
                {
                    Name = name,
                    Surname = surname,
                    UserName = name.ToLower() + "" + surname.ToLower(),
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, AccountRoles.Consumer.ToString());

                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                    if (!string.IsNullOrEmpty(url))
                    {
                        return Redirect(url);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("ConsumerSignup");
                }
            }

            public IActionResult CompanySignup() 
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
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
                    Company = company,
                    UserPhoto = "Default_pfp.svg.png"
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

            public IActionResult Signin()
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }
            return View();
            }

            [HttpPost]
            public async Task<IActionResult> Signin(LoginVM loginRequestUser)
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

            public async Task<IActionResult> Signout()
            {
                _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            public async Task<IActionResult> ForgotPassword(string email)
            {
                User user = await _userManager.FindByEmailAsync(email);

                if (user is not null)
                {
                    var token = await _userManager.FindByEmailAsync(email);
                    var url = Url.Action(
                            nameof(ResetPassword),
                            "Account",
                            new
                            {
                                user.Email,
                            },
                            Request.Scheme,
                            Request.Host.ToString()
                        );

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Reset Password";
                    message.Body = string.Empty;
                    string body = string.Empty;

                    using (StreamReader reader = new StreamReader("wwwroot/confirmationForReset.html"))
                    {
                        body = reader.ReadToEnd();
                    }
                    message.Body = body.Replace("{{link}}", url);
                    //message.Body = body.Replace("{{name}}", user.Name +" "+ user.Surname);
                    message.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential("dreamsrentofficial@gmail.com", "cxpstlrytkzgyrdk");
                    smtpClient.Send(message);
                }

                return RedirectToAction("Index", "Home");
            }

            public async Task<IActionResult> ResetPassword(string email)
            {
                ResetPasswordVM resetPasswordVM = new()
                {
                    Email = email,
                };
                return View(resetPasswordVM);
            }

            [HttpPost]
            public async Task<IActionResult> ResetingPassword(ResetPasswordVM resetPasswordVM)
            {
                User user = await _userManager.FindByEmailAsync(resetPasswordVM.Email);

                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordVM.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }

                _context.SaveChanges();
                return RedirectToAction("SignIn", "Account");
            }
        #endregion

        #region MyAccount

            public IActionResult ConsumerAccount(string UserName)
            {

                User? user = _context.Users
                    .Include(u=>u.PayCard).ThenInclude(p=>p.PayCardType)
                        .Include(u=>u.Wishlist).ThenInclude(w=>w.wishlistItems)
                            .Include(u=>u.Rents).ThenInclude(r=>r.Car)
                            .Include(u=>u.Orders).ThenInclude(o=>o.Car).ThenInclude(c=>c.Brand).ThenInclude(b=>b.Models)
                            .Include(u => u.Orders).ThenInclude(o => o.Car).ThenInclude(c => c.Company)
                                .FirstOrDefault(u => u.UserName == UserName);

                ConsumerAccountVM consumerAccountVM = new()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    UserPhoto = user.UserPhoto,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    PayCard = user.PayCard,
                    Wishlist = user.Wishlist,
                    Rents = user.Rents,
                    Orders = user.Orders,
                };
                
                return View(consumerAccountVM);
            }

            public async Task<IActionResult> ChangeConsumerPhoto(ConsumerAccountVM consumerAccountVM, string Id)
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == Id);

                string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");

                string removePath = Path.Combine(_env.WebRootPath, "assets", "images", "users", user.UserPhoto);

                user.UserPhoto = await consumerAccountVM.iff_UserPhoto.CreateImage(imageFolderPath, "users");

                FileUpload.DeleteImage(removePath);

                _context.SaveChanges();
                return RedirectToAction("ConsumerAccount", "Account", new {UserName = user.UserName});
            }

            public IActionResult ChangeConsumerDetails(ConsumerAccountVM consumerAccountVM)
            {
                User user = _context.Users.FirstOrDefault(u=>u.Id == consumerAccountVM.Id);

                if (consumerAccountVM.Name == null || consumerAccountVM.Surname == null || consumerAccountVM.Email == null || consumerAccountVM.PhoneNumber == null)
                {
                    return RedirectToAction("ConsumerAccount", "Account", new { UserName = user.UserName});
                }

                user.Name = consumerAccountVM.Name;
                user.Surname = consumerAccountVM.Surname;
                user.Email = consumerAccountVM.Email;
                user.PhoneNumber = consumerAccountVM.PhoneNumber;

                _context.SaveChanges();
                return RedirectToAction("ConsumerAccount", "Account", new { UserName = user.UserName });
            }

            public IActionResult AddDebitCard(string Id, string HolderName, string HolderSurname, string CardNumber, string Date, string date, string cvv)
            {
                User user = _context.Users.FirstOrDefault(u=>u.Id == Id);

                PayCard payCard = new()
                {
                    HolderName = HolderName.ToUpper(),
                    HolderSurname = HolderSurname.ToUpper(),
                    CardNumber = CardNumber.Replace(" ", ""),
                    Date = Date,
                    cvv = cvv
                };

                if (CardNumber.StartsWith("4"))
                {
                    PayCardType payCardType = _context.PayCardTypes.FirstOrDefault(p => p.Name == "Visa");
                    payCard.PayCardType = payCardType;
                }
                if (CardNumber.StartsWith("5"))
                {
                    PayCardType payCardType = _context.PayCardTypes.FirstOrDefault(p => p.Name == "Master");
                    payCard.PayCardType = payCardType;
                }

                user.PayCard = payCard;
                _context.SaveChanges();

                return RedirectToAction("ConsumerAccount", new {UserName = user.UserName});
            }

            public IActionResult EditDebitCard(string Id, string HolderName, string HolderSurname, string CardNumber, string Date, string date, string cvv) 
            {
                User user = _context.Users.Include(u=>u.PayCard).ThenInclude(p=>p.PayCardType).FirstOrDefault(u => u.Id == Id);
                
                user.PayCard.HolderName = HolderName;
                user.PayCard.HolderSurname = HolderSurname;
                user.PayCard.CardNumber = CardNumber.Replace(" ", "");
                user.PayCard.Date = Date;
                user.PayCard.cvv = cvv;

                if (CardNumber.StartsWith("4"))
                {
                    PayCardType payCardType = _context.PayCardTypes.FirstOrDefault(p => p.Name == "Visa");
                    user.PayCard.PayCardType = payCardType;
                }
                if (CardNumber.StartsWith("5"))
                {
                    PayCardType payCardType = _context.PayCardTypes.FirstOrDefault(p => p.Name == "Master");
                    user.PayCard.PayCardType = payCardType;
                }

                _context.SaveChanges();

                return RedirectToAction("ConsumerAccount", new { UserName = user.UserName });
            }

            public IActionResult CompanyAccount(string UserName)
            {
                ViewBag.Cars = _context.Cars
                .Include(c=>c.Transmission)
                    .Include(c=>c.FuelType)
                        .Include(c=>c.Engine)
                            .Include(c=>c.CarPhotos)
                                .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                                    .ToList();
                ViewBag.Ratings = _context.Ratings.Include(r=>r.Comment).ToList();
                ViewBag.Cities = _context.Cities.ToList();
                ViewBag.Streets = _context.Streets.ToList();

                if (UserName == null) { return RedirectToAction("NotFound", "Error"); }

                User? user = _context.Users.FirstOrDefault(u => u.UserName == UserName);

                if (user == null) { return RedirectToAction("NotFound", "Error"); }

                Company? company = _context.Companies
                .Include(c=>c.Cars)
                .Include(c=>c.companyPickupLocations).ThenInclude(cp=>cp.PickupLocation).ThenInclude(p=>p.City)
                .Include(c=>c.companyDropoffLocations).ThenInclude(cp=>cp.DropoffLocation).ThenInclude(p=>p.City)
                .Include(c=>c.Orders).ThenInclude(o=>o.Car).ThenInclude(c=>c.Brand).ThenInclude(b=>b.Models)
                .Include(c=>c.Orders).ThenInclude(o=>o.User)
                .Include(c=>c.Bookings).ThenInclude(b => b.car).ThenInclude(c => c.Brand).ThenInclude(b => b.Models)
                .Include(c=>c.Bookings).ThenInclude(b=>b.car).ThenInclude(x=>x.CarPhotos)
                .Include(c=>c.Bookings).ThenInclude(b => b.User)
                .FirstOrDefault(c=>c.UserId == user.Id);

                //Company company = user.Company;

                return View(company);
            }

            [HttpPost]
            public async Task<IActionResult> ChangeCompanyPhoto(Company changedCompany, int Id)
            {
                if (Id == 0) { RedirectToAction("NotFound", "Error"); }

                Company? company = _context.Companies.FirstOrDefault(c => c.Id == Id);

                if (company == null) { RedirectToAction("NotFound", "Error"); }

                string imageFolderPath = Path.Combine(_env.WebRootPath, "assets", "images");

                string removePath = Path.Combine(_env.WebRootPath, "assets", "images", "users", company.CompanyPhoto);

                FileUpload.DeleteImage(removePath);

                company.CompanyPhoto = await changedCompany.iff_CompanyPhoto.CreateImage(imageFolderPath, "users");

                User user = _context.Users.FirstOrDefault(u => u.Id == company.UserId);

                user.UserPhoto = company.CompanyPhoto;

                _context.SaveChanges();
                return RedirectToAction("CompanyAccount", "Account", new { UserName = user.UserName });
            }

            public async Task<IActionResult> ChangeCompanyDetails(Company company, string email, string phone, string location, int Id)
            {
                if (Id == 0) { return RedirectToAction("NotFound", "Error"); }

                Company? findCompany = _context.Companies.Include(c=>c.User).FirstOrDefault(c=>c.Id == Id);

                if (findCompany == null) { return RedirectToAction("NotFound", "Error"); }

                findCompany.CompanyName = company.CompanyName;
                findCompany.User.PhoneNumber = phone;
                findCompany.Location = location;

                if (findCompany.User.Email != email)
                {
                    findCompany.User.Email = email;
                    findCompany.User.NormalizedEmail = email.ToUpper();
                    findCompany.User.EmailConfirmed = false;
                    
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(findCompany.User);
                    var url = Url.Action(
                            nameof(ConfirmEmail),
                            "Account",
                            new
                            {
                                findCompany.User.Email,
                                token
                            },
                            Request.Scheme,
                            Request.Host.ToString()
                        );

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
                    message.To.Add(new MailAddress(findCompany.User.Email));
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
                }

                _context.SaveChanges();
                return RedirectToAction("CompanyAccount", "Account", new { UserName = findCompany.User.UserName });
            }
     
            public async Task<IActionResult> AddPickupLocation(int Id, int cityId, int streetId)
            {
                if (Id == 0) { return RedirectToAction("NotFound", "Error"); }

                Company? company = _context.Companies
                    .Include(c=>c.User)
                        .Include(c=>c.companyPickupLocations).ThenInclude(c=>c.PickupLocation)
                            .FirstOrDefault(c => c.Id == Id);
                

                if (company == null) { return RedirectToAction("NotFound", "Error"); }

                City city = _context.Cities.FirstOrDefault(c => c.Id == cityId);

                PickupLocation pickupLocation = new();

                CompanyPickupLocation companyPickupLocation = new();

                Street findStreet = _context.Streets.FirstOrDefault(s => s.Id == streetId);

                if (company.companyPickupLocations.Any(c=>c.PickupLocation.StreetId == streetId))
                { 
                    return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
                }

                if (_context.PickupLocations.Any(p => p.StreetId == streetId && p.CityId == cityId))
                {
                    PickupLocation pickup = _context.PickupLocations.FirstOrDefault(p => p.StreetId == streetId && p.CityId == cityId);
                    companyPickupLocation.PickupLocation = pickup;
                    company.companyPickupLocations.Add(companyPickupLocation);

                    _context.SaveChanges();
                    return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
                }

                pickupLocation.CityId = city.Id;
                pickupLocation.StreetId = findStreet.Id;

                companyPickupLocation.PickupLocation = pickupLocation;
                companyPickupLocation.Company = company;

                company.companyPickupLocations.Add(companyPickupLocation);
                pickupLocation.companyPickupLocations.Add(companyPickupLocation);
                _context.SaveChanges();

                return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
            }

            public async Task<IActionResult> AddDropoffLocation(int Id, int cityId, int streetId)
            {
                if (Id == 0) { return RedirectToAction("NotFound", "Error"); }

                Company? company = _context.Companies
                    .Include(c => c.User)
                        .Include(c => c.companyDropoffLocations).ThenInclude(c => c.DropoffLocation)
                            .FirstOrDefault(c => c.Id == Id);

                if (company == null) { return RedirectToAction("NotFound", "Error"); }

                City city = _context.Cities.FirstOrDefault(c => c.Id == cityId);

                DropoffLocation dropoffLocation = new();

                CompanyDropoffLocation companyDropoffLocation = new();

                Street findStreet = _context.Streets.FirstOrDefault(s => s.Id == streetId);

                if (company.companyDropoffLocations.Any(c => c.DropoffLocation.StreetId == streetId))
                {
                    return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
                }

                if (_context.DropoffLocations.Any(p => p.StreetId == streetId && p.CityId == cityId))
                {
                    DropoffLocation dropoff = _context.DropoffLocations.FirstOrDefault(p => p.StreetId == streetId && p.CityId == cityId);
                    companyDropoffLocation.DropoffLocation = dropoff;
                    company.companyDropoffLocations.Add(companyDropoffLocation);

                    _context.SaveChanges();
                    return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
                }

                dropoffLocation.CityId = city.Id;
                dropoffLocation.StreetId = findStreet.Id;

                companyDropoffLocation.DropoffLocation = dropoffLocation;
                companyDropoffLocation.Company = company;

                company.companyDropoffLocations.Add(companyDropoffLocation);
                dropoffLocation.companyDropoffLocations.Add(companyDropoffLocation);
                _context.SaveChanges();

                return RedirectToAction("CompanyAccount", "Account", new { UserName = company.User.UserName });
            }

            public IActionResult AddCompanyLocation(int companyId, string location)
            {
                Company company = _context.Companies.Include(c=>c.User).FirstOrDefault(c=>c.Id == companyId);
                company.Location = location;
                _context.SaveChanges();

                return RedirectToAction("CompanyAccount", "Account", new {UserName = company.User.UserName});
            }
        #endregion
        public IActionResult GetStreets(int cityId)
        {
            List<Street> streets = _context.Streets.Where(m => m.CityId.Equals(cityId)).ToList();

            if (streets == null)
            {
                return Json(new { status = 404 });
            }

            return Json(streets);
        }

        public async Task AddRoles()
        {
            //await _roleManager.CreateAsync(new IdentityRole(AccountRoles.Admin.ToString()));
            //await _roleManager.CreateAsync(new IdentityRole(AccountRoles.Consumer.ToString()));
            //await _roleManager.CreateAsync(new IdentityRole(AccountRoles.Company.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(AccountRoles.SuperAdmin.ToString()));
        }
    }
}
