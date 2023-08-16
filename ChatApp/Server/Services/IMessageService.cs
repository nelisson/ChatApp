using ChatApp.Shared.Model;

namespace ChatApp.Server.Services
{
    public interface IMessageService
    {
        Task<int> SaveMessageAsync(Message message);
        List<Message> GetMessages(int chatroomId);
    }
}
