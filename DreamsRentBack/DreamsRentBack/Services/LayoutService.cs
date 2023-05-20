using DreamsRentBack.DAL;
using DreamsRentBack.Entities;
using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Identity;

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

        public List<Body> GetBodyTypes()
        {
            List<Body> bodies = _context.Bodys.Take(5).ToList();

            return bodies;
        } 
    }
}
