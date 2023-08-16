using ChatApp.Shared.Model;

namespace ChatApp.Server.Services
{
    public interface IMessageService
    {
        Task SaveMessageAsync(Message message);
    }
}
