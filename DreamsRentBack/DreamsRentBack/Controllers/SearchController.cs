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
            List<string> names = streets.Select(s=>s.Name).ToList();
            return names;
        }
    }
}
