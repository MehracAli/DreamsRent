namespace DreamsRentBack.Entities.ClientModels
{
    public class Wishlist:BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public List<WishlistItem> wishlistItems {  get; set; }

        public Wishlist()
        {
            wishlistItems = new();
        }
    }
}
