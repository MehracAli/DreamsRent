namespace DreamsRentBack.Entities.ClientModels
{
    public class PickupLocation:BaseEntity
    {
        public int CityId { get; set; }
        public int StreetId { get; set; }
        public City City { get; set; }
        public List<CompanyPickupLocation> companyPickupLocations { get; set; }

        public PickupLocation()
        {
            companyPickupLocations = new();
        }
    }
}
