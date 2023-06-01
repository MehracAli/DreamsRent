using DreamsRentBack.Entities.CarModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities.ClientModels
{
    public class Company:BaseEntity
    {
        public string CompanyName { get; set; }
        public bool Verification { get; set; } = false;
        public string? CompanyPhoto { get; set; }
        [NotMapped]
        public IFormFile? iff_CompanyPhoto { get; set; }
        public int? LocationId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Location? Location { get; set; }
        public List<CompanyPickupLocation> companyPickupLocations { get; set; }
        public List<CompanyDropoffLocation> companyDropoffLocations { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<Car> Cars { get; set; }
        public List<Order> Orders { get; set; }
        public Company()
        {
            Cars = new();
            companyPickupLocations = new();
            companyDropoffLocations = new();
            Orders = new List<Order>();
            Bookings = new();
        }

    }
}
