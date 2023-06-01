using DreamsRentBack.Entities.CarModels;

namespace DreamsRentBack.Entities.ClientModels
{
    public class Order:BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
