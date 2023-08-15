namespace ChatApp.Shared.Model
{
    public class Chatroom
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public virtual required List<Message> Messages { get; set; }
    }
}
