namespace DreamsRentBack.Entities.ClientModels
{
    public class ChatMessage:BaseEntity
    {
        public int ChatId { get; set; }
        public Chat chat { get; set; }
        public int MessageId { get; set; }
        public Message message { get; set; }
    }
}
