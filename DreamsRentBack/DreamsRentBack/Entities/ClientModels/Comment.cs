using DreamsRentBack.Entities.CarModels;

namespace DreamsRentBack.Entities.ClientModels
{
    public class Comment:BaseEntity
    {
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
