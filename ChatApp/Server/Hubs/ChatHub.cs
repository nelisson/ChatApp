using ChatApp.Server.Data;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatHub(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SendMessage(int chatroomId, string userId, string message)
        {
            var newMessage = new Message
            {
                ChatroomId = chatroomId,
                UserId = userId,
                Content = message,
                Timestamp = DateTime.Now
            };
            _dbContext.Messages.Add(newMessage);
            await _dbContext.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", chatroomId);
        }
    }
}
