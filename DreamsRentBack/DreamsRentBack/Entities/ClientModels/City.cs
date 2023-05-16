namespace DreamsRentBack.Entities.ClientModels
{
    public class City:BaseEntity
    {
        public string Name { get; set; }
        public List<Street> Streets { get; set; }
        public List<Location> Locations { get; set; }

        public City()
        {
            Streets = new();
            Locations = new();
        }
    }
}
