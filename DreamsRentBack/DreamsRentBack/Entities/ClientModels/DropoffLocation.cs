namespace DreamsRentBack.Entities.ClientModels
{
    public class DropoffLocation:BaseEntity
    {
        public int CityId { get; set; }
        public int StreetId { get; set; }
        public City City { get; set; }
        public List<CompanyDropoffLocation> companyDropoffLocations { get; set; }

        public DropoffLocation()
        {
            companyDropoffLocations = new();
        }
    }
}
