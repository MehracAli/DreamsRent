namespace DreamsRentBack.Entities.CarModels
{
    public class Body:BaseEntity
    {
        public string Name { get; set; }
        public List<Car> Cars { get; set; }

        public Body()
        {
            Cars = new();
        }
    }
}
