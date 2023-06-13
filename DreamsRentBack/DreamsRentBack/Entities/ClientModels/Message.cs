namespace DreamsRentBack.Entities.ClientModels
{
    public class Message:BaseEntity
    {
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<ChatMessage> Chats { get; set; }

        public Message()
        {
            Chats = new();
        }
    }
}
