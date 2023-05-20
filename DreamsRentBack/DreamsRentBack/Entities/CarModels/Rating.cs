using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.Entities.CarModels
{
    public class Rating:BaseEntity
    {
        public double Point { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public User User { get; set; }
        public Comment Comment { get; set; }

    }
}
