using ChatApp.Shared.Model;

namespace ChatApp.Server.Services
{
    public interface IChatroomService
    {
        IEnumerable<Chatroom> GetChatrooms();
        Task<Chatroom> CreateChatroom();
    }
}
