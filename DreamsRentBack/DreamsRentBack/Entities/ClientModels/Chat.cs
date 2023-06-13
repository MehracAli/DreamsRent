namespace DreamsRentBack.Entities.ClientModels
{
    public class Chat:BaseEntity
    {
        public string PartnerId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public DateTime IsLast { get; set; }

        public Chat()
        {
            Messages = new();
        }
    }
}
