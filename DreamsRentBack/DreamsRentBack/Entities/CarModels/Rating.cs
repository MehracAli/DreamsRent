using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.Entities.CarModels
{
    public class Rating:BaseEntity
    {
        public double Point { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
