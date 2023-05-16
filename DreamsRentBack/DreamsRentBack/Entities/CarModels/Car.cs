using DreamsRentBack.Entities.ClientModels;

namespace DreamsRentBack.Entities.CarModels
{
    public class Car : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Views { get; set; }
        public byte Rating { get; set; }
        public byte Capacity { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<CarPhoto> CarPhotos { get; set; }
        public bool Availability { get; set; } = true;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }

        //Extra Services Start
        public List<ExtraServicesAndCars> ServicesAndCars { get; set; }
        //Extra Services End

        //Specifications Start
        public int BodyId { get; set; }
        public Body Body { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int TransmissionId { get; set; }
        public Transmission Transmission { get; set; }
        public int FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        public byte Speed { get; set; }
        public int DrivertrianId { get; set; }
        public Drivetrian Drivetrian { get; set; }
        public byte Year { get; set; }
        public int AirConditionId { get; set; }
        public AirCondition AirCondition { get; set;}
        public string VIN { get; set; }
        public byte Doors { get; set; }
        public int BreakId { get; set; }
        public Brake Brake { get; set; }
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        //Specifications End

        //Features Start
        public List<CarFeaturesAndCars> FeaturesAndCars { get; set; }
        //Features End

        //Comment Start
        public List<Comment> Comments { get; set; }
        public Car()
        {
            CarPhotos = new();
            ServicesAndCars = new();
            FeaturesAndCars = new();
            Comments = new();
        }
    }
}
