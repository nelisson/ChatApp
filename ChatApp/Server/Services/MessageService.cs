using ChatApp.Server.Data;
using ChatApp.Shared.Model;

namespace ChatApp.Server.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _dbContext;

        public MessageService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveMessageAsync(Message message)
        {
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}
