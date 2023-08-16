using ChatApp.Shared.Model;

namespace ChatApp.Server.Services
{
    public class MessageService : IMessageService
    {
        private readonly IApplicationDbContext _dbContext;

        public MessageService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveMessageAsync(Message message)
        {
            _dbContext.Messages.Add(message);
            return _dbContext.SaveChangesAsync();
        }

        public List<Message> GetMessages(int chatroomId)
        {
            return _dbContext.Messages
                .Where(m => m.ChatroomId == chatroomId)
                .ToList();
        }
    }
}
