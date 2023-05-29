using DreamsRentBack.Entities.CarModels;

namespace DreamsRentBack.Entities.ClientModels
{
    public class Booking:BaseEntity
    {
        public int CarId { get; set; }
        public Car car { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int CompanyId { get; set; }
        public Company company { get; set; }
    }
}
