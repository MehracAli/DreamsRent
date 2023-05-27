using Microsoft.AspNetCore.SignalR;

namespace DreamsRentBack.Hubs
{
    public class ChatHub:Hub
    {
        public Task SendMEssage(string message)
        {
            return Clients.Others.SendAsync("send", message);
        }
    }
}
