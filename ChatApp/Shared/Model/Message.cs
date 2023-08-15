namespace ChatApp.Shared.Model
{
    public class Message
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public required string Content { get; set; }
        public int ChatroomId { get; set; }
        public required virtual Chatroom Chatroom { get; set; }
    }
}
