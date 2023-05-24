namespace DreamsRentBack.Entities.ClientModels
{
    public class CompanyPickupLocation:BaseEntity
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int PickupLocationId { get; set; }
        public PickupLocation PickupLocation { get; set; }
    }
}
