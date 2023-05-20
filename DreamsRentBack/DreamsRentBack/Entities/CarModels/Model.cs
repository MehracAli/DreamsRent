namespace DreamsRentBack.Entities.CarModels
{
    public class Model:BaseEntity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
