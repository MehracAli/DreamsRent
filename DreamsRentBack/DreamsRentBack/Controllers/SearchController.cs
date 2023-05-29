using DreamsRentBack.DAL;
using DreamsRentBack.Entities.ClientModels;
using Microsoft.AspNetCore.Mvc;

namespace DreamsRentBack.Controllers
{
    public class SearchController : Controller
    {
        public DreamsRentDbContext _context { get; set; }

        public SearchController(DreamsRentDbContext context)
        {
            _context = context;
        }

        public List<string> GetMatchingData(string searchQuery)
        {
            List<Street> streets = _context.Streets.Where(s=>s.Name.Contains(searchQuery)).ToList();

            City city = _context.Cities.FirstOrDefault(c => c.Streets.Any(s => s.Name.Contains(searchQuery)));

            List<string> names = streets.Select(s => city.Name + ", " + s.Name).ToList();

            if (_context.Cities.Any(c => c.Name.Contains(searchQuery)))
            {
                List<City> queryCity = _context.Cities.Where(c => c.Name.Contains(searchQuery)).ToList();

                List<Street> streets1 = _context.Streets.Where(s => queryCity.Any(c => c.Id == s.CityId)).ToList();
                foreach (var item in streets1)
                {
                    names = queryCity.Select(c => c.Name + ", " + item.Name).ToList();
                }
            }
            return names;
        }
    }
}
