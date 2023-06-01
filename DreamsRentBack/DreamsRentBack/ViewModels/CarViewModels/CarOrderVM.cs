using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.CarViewModels
{
    public class CarOrderVM
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public List<string> extraServices { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public double Price { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
    }
}
