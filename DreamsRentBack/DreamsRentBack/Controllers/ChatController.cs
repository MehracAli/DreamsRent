using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using DreamsRentBack.Hubs;
using DreamsRentBack.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace DreamsRentBack.Controllers
{
    public class ChatController : Controller
    {
        private readonly DreamsRentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(DreamsRentDbContext context, UserManager<User> userManager, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
        }

        public IActionResult Chatting(int chatId)
        {
            Chat chat = _context.Chats.Include(c=>c.User).Include(c=>c.Messages).FirstOrDefault(c => c.Id == chatId);
            User user = _context.Users.Include(u=>u.Chats).ThenInclude(c=>c.Messages).FirstOrDefault(user=>user.Id == chat.UserId);

            ViewBag.Messages = _context.Messages.ToList();

            ViewBag.Partner = _context.Users
                .Include(u=>u.Company)
                    .Include(u=>u.Chats)
                        .ThenInclude(c=>c.Messages)
                            .FirstOrDefault(u => u.Id == chat.PartnerId);

            ViewBag.Chats = user.Chats.ToList();
            ViewBag.Users = _context.Users.Include(u=>u.Company).ToList();

            return View(chat);
        }

        public IActionResult ChattingCompany(int chatId)
        {
            Chat chat = _context.Chats.Include(c => c.Messages).FirstOrDefault(c => c.Id == chatId);
            User user = _context.Users.Include(u => u.Chats).ThenInclude(c => c.Messages).FirstOrDefault(user => user.Id == chat.UserId);

            ViewBag.Messages = _context.Messages.ToList();

            ViewBag.Partner = _context.Users
                .Include(u => u.Company)
                    .Include(u => u.Chats)
                        .ThenInclude(c => c.Messages)
                            .FirstOrDefault(u => u.Id == chat.PartnerId);

            ViewBag.Chats = user.Chats.ToList();
            ViewBag.Users = _context.Users.Include(u => u.Company).ToList();

            return View(chat);
        }

        public IActionResult CreateChat(int companyId)
        {
            User user = _context.Users.Include(u=>u.Chats).FirstOrDefault(u => u.UserName == User.Identity.Name);
            Company company = _context.Companies.Include(c=>c.User).FirstOrDefault(c=>c.Id == companyId);

            if (user.Chats.Any(c=>c.PartnerId == company.UserId))
            {
                Chat alreadyChat = user.Chats.FirstOrDefault(c => c.PartnerId == company.UserId);

                return RedirectToAction(nameof(Chatting), new { chatId = alreadyChat.Id });
            }

            Chat myChat = new()
            {
                PartnerId = company.UserId,
                UserId = user.Id
            };

            Chat partnerChat = new()
            {
                PartnerId = user.Id,
                UserId = company.UserId,
            };
            
            user.Chats.Add(myChat);
            company.User.Chats.Add(partnerChat);

            _context.SaveChanges();
            return RedirectToAction(nameof(Chatting), new {chatId = myChat.Id});
        }

        public async Task<IActionResult> SendMessage(int chatId, string message)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            Chat chat = _context.Chats.Include(c=>c.Messages).FirstOrDefault(c=>c.Id == chatId);

            ViewBag.Messages = _context.Messages.ToList();

            ViewBag.Partner = _context.Users
                .Include(u => u.Company)
                    .Include(u => u.Chats)
                        .ThenInclude(c => c.Messages)
                            .FirstOrDefault(u => u.Id == chat.PartnerId);

            ViewBag.Chats = user.Chats.ToList();
            ViewBag.Users = _context.Users.Include(u => u.Company).ToList();

            User partner = _context.Users.Include(p=>p.Chats).ThenInclude(c=>c.Messages).FirstOrDefault(u => u.Id == chat.PartnerId);
            Chat partnersChat = partner.Chats.FirstOrDefault(c=>c.PartnerId == user.Id);

            Entities.ClientModels.Message newMessage = new()
            {
                UserName = User.Identity.Name,
                Text = message,
                TimeStamp = DateTime.Now
            };
            _context.Messages.Add(newMessage);
            _context.SaveChanges();

            ChatMessage myChat = new()
            {
                ChatId = chatId,
                MessageId = newMessage.Id
            };

            ChatMessage partnerChat = new()
            {
                ChatId = partnersChat.Id,
                MessageId = newMessage.Id
            };

            DateTime sendingTime = DateTime.Now;

            await _context.ChatMessages.AddAsync(myChat);
            await _context.ChatMessages.AddAsync(partnerChat);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.Users(user.Id,partner.Id).SendAsync("ReceiveMessage", message, sendingTime);

            if (User.IsInRole("Company"))
            {
                return PartialView("_companyChatPartial", chat);
            }

            return PartialView("_chatPartial", chat);
        }

        //[HttpPost]
        //public ActionResult SaveVoiceRecording(IFormFile voiceRecording)
        //{
        //    if (voiceRecording != null && voiceRecording.Length > 0)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            voiceRecording.CopyTo(memoryStream);
        //            byte[] voiceData = memoryStream.ToArray();

        //            return Content("Voice recording saved successfully.");
        //        }
        //    }

        //    return Content("Failed to save voice recording.");
        //}

    }
}
