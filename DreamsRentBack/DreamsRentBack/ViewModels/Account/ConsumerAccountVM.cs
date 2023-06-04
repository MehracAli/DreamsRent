using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.Account
{
    public class ConsumerAccountVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserPhoto { get; set; }
        public IFormFile iff_UserPhoto { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public PayCard PayCard { get; set; }
        public Wishlist Wishlist { get; set; }
        public List<Rent> Rents { get; set;}
        public List<Order> Orders { get; set; }
    }
}
