namespace DreamsRentBack.Entities.CarModels
{
    public class Brake:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set;}
        public Brake()
        {
            Cars = new();
        }
    }
}
