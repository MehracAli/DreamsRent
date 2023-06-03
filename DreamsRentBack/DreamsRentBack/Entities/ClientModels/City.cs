namespace DreamsRentBack.Entities.ClientModels
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public List<Street> Streets { get; set; }

        public City()
        {
            Streets = new();
        }
    }
}
