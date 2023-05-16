namespace DreamsRentBack.Entities.CarModels
{
    public class Drivetrian:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
        public Drivetrian()
        {
            Cars = new();
        }
    }
}
