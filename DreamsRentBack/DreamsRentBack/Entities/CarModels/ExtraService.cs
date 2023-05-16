namespace DreamsRentBack.Entities.CarModels
{
    public class ExtraService:BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public List<ExtraServicesAndCars> ServicesAndCars { get; set; }

        public ExtraService()
        {
            ServicesAndCars = new();
        }
    }
}
