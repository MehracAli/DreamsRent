namespace DreamsRentBack.Entities.CarModels
{
    public class ExtraServicesAndCars:BaseEntity
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int ExtraServiceId { get; set; }
        public ExtraService ExtraService { get; set; }
    }
}
