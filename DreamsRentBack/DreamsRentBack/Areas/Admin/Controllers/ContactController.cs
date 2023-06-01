using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;

namespace DreamsRentBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        public IActionResult ShowMessage(string email, string comment, string name)
        {
            ViewBag.Email = email;
            ViewBag.Comment = comment;
            ViewBag.Name = name;
            return View();
        }

        public IActionResult SendMessage(string email, string comment)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("dreamsrentofficial@gmail.com", "DreamsRent");
            message.To.Add(new MailAddress(email));
            message.Subject = "DreamsRent Support";
            message.Body = string.Empty;
            message.Body = $"{comment}";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential("dreamsrentofficial@gmail.com", "cxpstlrytkzgyrdk");
            smtpClient.Send(message);

            return RedirectToAction("Index", "Home");
        }
    }
}
