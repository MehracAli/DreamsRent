namespace DreamsRentBack.Entities.CarModels
{
    public class AirCondition:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

        public AirCondition()
        {
            Cars = new();
        }
    }
}
