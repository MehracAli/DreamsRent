namespace DreamsRentBack.Entities.CarModels
{
    public class CarFeaturesAndCars:BaseEntity
    {
        public int CarFeaturesId { get; set; }
        public CarFeatures CarFeatures { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
