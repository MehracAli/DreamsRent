using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.CarViewModels
{
    public class CarCheckoutVM
    {
        public int Id { get; set; }
        public string PickLocation { get; set; }
        public string DropLocation { get; set;}
        public DateTime PickDate { get; set; }
        public DateTime DropDate { get; set; }
        public double Price { get; set; }
        public User User { get; set; }
    }
}
