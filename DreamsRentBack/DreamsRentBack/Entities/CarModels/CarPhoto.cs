namespace DreamsRentBack.Entities.CarModels
{
    public class CarPhoto:BaseEntity
    {
        public string Path { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
