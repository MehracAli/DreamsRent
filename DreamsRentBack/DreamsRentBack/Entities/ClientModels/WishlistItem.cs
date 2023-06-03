using DreamsRentBack.Entities.CarModels;

namespace DreamsRentBack.Entities.ClientModels
{
    public class WishlistItem:BaseEntity
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string CarPhoto { get; set; }
        public double Price { get; set; }
        public string CompanyName { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
