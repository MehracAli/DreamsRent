namespace DreamsRentBack.Entities.CarModels
{
    public class Transmission:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

        public Transmission()
        {
            Cars = new();
        }
    }
}
