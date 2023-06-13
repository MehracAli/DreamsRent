using DreamsRentBack.DAL;
using DreamsRentBack.Entities;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DreamsRentBack.Services
{
    public class LayoutService
    {
        public DreamsRentDbContext _context { get; set; }
        public IHttpContextAccessor _accessor { get; set; }
        private readonly UserManager<User> _userManager;


        public LayoutService(DreamsRentDbContext context, IHttpContextAccessor accessor, UserManager<User> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }

        public List<Setting> GetSettings()
        {
            List<Setting> settings = _context.Settings.ToList();

            return settings;
        }

        public async Task<string> GetConsmuerFullName()
        {
            User user = await _userManager.GetUserAsync(_accessor.HttpContext.User);
            return user?.Name+" "+user?.Surname;
        }

        public async Task<string> GetCompanyName()
        {
            List<Company> companies = _context.Companies.ToList();

            User user = await _userManager.GetUserAsync(_accessor.HttpContext.User);

            Company company = companies.FirstOrDefault(c=>c.UserId.Equals(user.Id));

            return company.CompanyName;
        }

        public async Task<int> GetChatId()
        {
            User user = await _userManager.GetUserAsync(_accessor.HttpContext.User);

            User contextUser = _context.Users.Include(u=>u.Chats).FirstOrDefault(u=>u.Id == user.Id);

            Chat chat = contextUser.Chats.OrderByDescending(c=>c.Id).FirstOrDefault(c => c.UserId == user.Id);

            if (chat == null)
            {
                return 0;
            }

            int chatId = chat.Id;

            return chatId;
        }

        public List<Body> GetBodyTypes()
        {
            List<Body> bodies = _context.Bodys.Take(5).ToList();

            return bodies;
        } 
    }
}
