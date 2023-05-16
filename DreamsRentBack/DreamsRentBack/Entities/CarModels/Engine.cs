namespace DreamsRentBack.Entities.CarModels
{
    public class Engine:BaseEntity
    {
        public string HorsePower { get; set; }
        public List<Car> Cars { get; set; }

        public Engine()
        {
            Cars = new();
        }
    }
}
