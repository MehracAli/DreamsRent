namespace DreamsRentBack.Entities.ClientModels
{
    public class PayCardType:BaseEntity
    {
        public string Name { get; set; }
        public List<PayCard> PayCards { get; set; }

        public PayCardType()
        {
            PayCards = new();
        }
    }
}
