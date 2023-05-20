using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.Entities.CarModels
{
    public class Like:BaseEntity
    {
        public int LikeCount { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public User User { get; set;}
    }
}
