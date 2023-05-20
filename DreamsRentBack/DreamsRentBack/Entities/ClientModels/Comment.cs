using DreamsRentBack.Entities.CarModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities.ClientModels
{
    public class Comment:BaseEntity
    {
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        [ForeignKey("Rating")]
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
    }
}
