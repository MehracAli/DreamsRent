using Microsoft.AspNetCore.SignalR;

namespace DreamsRentBack.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMEssage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
