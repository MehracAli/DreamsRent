namespace DreamsRentBack.Entities.ClientModels
{
    public class CompanyDropoffLocation:BaseEntity
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int DropoffLocationId { get; set; }
        public DropoffLocation DropoffLocation { get; set; }
    }
}
