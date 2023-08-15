using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatroomId, string userId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatroomId, userId, message);
        }
    }
}
