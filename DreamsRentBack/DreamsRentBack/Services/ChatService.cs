using DreamsRentBack.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DreamsRentBack.Services
{
    public class ChatService
    {
        readonly IHubContext<ChatHub> _hubContext;

        public ChatService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("receiveMessage", message);
        }
    }
}
