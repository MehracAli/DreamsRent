namespace DreamsRentBack.Entities.ClientModels
{
    public class Location:BaseEntity
    {
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Company> Companies { get; set; }

        public Location()
        {
            Companies = new();
        }
    }
}
