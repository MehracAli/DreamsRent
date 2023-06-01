using DreamsRentBack.Entities.CarModels;
using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.ViewModels.CarViewModels
{
    public class CarDetailVM
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public Brand Brand { get; set; }
        public int ModelId { get; set; }
        public Location Location { get; set; }
        public int Views { get; set; }
        public List<CarPhoto> CarPhotos { get; set; }
        public List<ExtraServicesAndCars> ServicesAndCars { get; set; }
        public Body Body { get; set; }
        public Transmission Transmission { get; set; }
        public FuelType FuelType { get; set; }
        public int Speed { get; set; }
        public Drivetrian Drivetrian { get; set; }
        public int Year { get; set; }
        public AirCondition AirCondition { get; set; }
        public string VIN { get; set; }
        public int Door { get; set; }
        public int BrakeId { get; set; }
        public Brake Brake { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        public List<CarFeaturesAndCars> FeaturesAndCars { get; set; }
        public List<Comment> Comments { get; set; }
        public string Description { get; set; }
        public Company Company { get; set; }
        public List<Booking> Bookings { get; set; }
        public bool Availability { get; set; }
    }
}
