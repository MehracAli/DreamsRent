using DreamsRentBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Controllers
{
    public class ChatController : Controller
    {
        readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
