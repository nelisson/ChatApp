using ChatApp.Shared.Model;

namespace ChatApp.Server.Services
{
    public class ChatroomService : IChatroomService
    {
        private readonly IApplicationDbContext _dbContext;

        public ChatroomService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Chatroom> GetChatrooms()
        {
            return _dbContext.Chatrooms.ToList();
        }

        public async Task<Chatroom> CreateChatroom()
        {
            var chatroomName = $"Chatroom_{DateTime.UtcNow:yyyyMMddHHmmssfff}";
            var chatroom = new Chatroom { Name = chatroomName };

            _dbContext.Chatrooms.Add(chatroom);
            await _dbContext.SaveChangesAsync();

            return chatroom;
        }
    }

}
