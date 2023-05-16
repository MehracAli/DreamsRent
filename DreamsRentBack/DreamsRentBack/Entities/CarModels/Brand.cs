namespace DreamsRentBack.Entities.CarModels
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
        public Brand()
        {
            Cars = new();
        }
    }
}
