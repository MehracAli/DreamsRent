namespace DreamsRentBack.Entities.CarModels
{
    public class CarFeatures:BaseEntity
    {
        public string Name { get; set; }
        public List<CarFeaturesAndCars> FeaturesAndCars { get; set; }

        public CarFeatures()
        {
            FeaturesAndCars = new();   
        }
    }
}
