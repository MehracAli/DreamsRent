namespace DreamsRentBack.Entities.CarModels
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string BrandLogo { get; set; }
        public List<Model> Models { get; set; }
        public List<Car> Cars { get; set; }
        public Brand()
        {
            Cars = new();
            Models = new();
        }
    }
}
