using DreamsRentBack.DAL;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StickerController : Controller
    {
        public DreamsRentDbContext _context;

        public StickerController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public IActionResult Detail(int carId)
        {
            Car? car = _context.Cars
                .Include(c=>c.CarPhotos)
                .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                .Include(c=>c.Engine)
                .Include(c=>c.FuelType)
                .Include(c=>c.Brake)
                .Include(c=>c.Transmission)
                .Include(c=>c.Body)
                .Include(c=>c.Drivetrian)
                .Include(c=>c.AirCondition)
                .Include(c=>c.ServicesAndCars).ThenInclude(s=>s.ExtraService)
                .Include(c=>c.FeaturesAndCars).ThenInclude(s=>s.CarFeatures)
                .FirstOrDefault(c=>c.Id == carId);

            return View(car);
        }

        public IActionResult Accept(int carId)
        {
            Car? car = _context.Cars
                .Include(c=>c.Brand).ThenInclude(b=>b.Models)
                .Include(c=>c.Company)
                .FirstOrDefault(car => car.Id == carId);

            car.CarConfirmation = CarConfirmation.Confirmed;

            List<Subscribe> subscribes =  _context.Subscribes.ToList();

            foreach (Subscribe subscribe in subscribes)
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
                message.To.Add(new MailAddress(subscribe.Email));
                message.Subject = "DreamsRent new car";
                message.Body = string.Empty;
                message.Body = $"Related new car {car.Brand.Name+" "+car.Brand.Models.FirstOrDefault(m=>m.Id == car.ModelId).Name+" from "+car.Company.CompanyName+ " link: https://localhost:7260/Detail/CarDetail/"+car.Id}";

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
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Reject(int carId)
        {
            Car? car = _context.Cars.FirstOrDefault(car => car.Id == carId);

            car.CarConfirmation = CarConfirmation.Negate;

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
