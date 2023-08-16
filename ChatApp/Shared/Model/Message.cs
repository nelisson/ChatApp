namespace ChatApp.Shared.Model
{
    public class Message
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int ChatroomId { get; set; }
        public required string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
