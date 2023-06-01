namespace DreamsRentBack.Entities.ClientModels
{
    public class ContactUs:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Comment { get; set; }
        public DateTime SendTime { get; set; }
    }
}
