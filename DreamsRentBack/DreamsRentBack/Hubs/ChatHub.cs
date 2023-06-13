using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;

namespace DreamsRentBack.Hubs
{
    public class ChatHub:Hub
    {
        private readonly DreamsRentDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChatHub(DreamsRentDbContext context, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task OnConnectedAsync()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (_httpContextAccessor.HttpContext.User.IsInRole("Consumer") || _httpContextAccessor.HttpContext.User.IsInRole("Company"))
                {
                    user.ConnectionId = Context.ConnectionId;
                }
                await _userManager.UpdateAsync(user);
                await Clients.All.SendAsync("online", user.Id);

            }

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

                if (_httpContextAccessor.HttpContext.User.IsInRole("Member"))
                {
                    user.ConnectionId = null;
                }
                await _userManager.UpdateAsync(user);
                await Clients.All.SendAsync("offline", user.Id);

            }

        }
    }
}
