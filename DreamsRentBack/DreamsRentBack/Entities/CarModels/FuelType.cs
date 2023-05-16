namespace DreamsRentBack.Entities.CarModels
{
    public class FuelType:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

        public FuelType()
        {
            Cars = new();
        }
    }
}
