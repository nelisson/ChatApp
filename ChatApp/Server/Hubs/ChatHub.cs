using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(int chatroomId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatroomId, Context.User?.Identity?.Name, message);
        }
    }
}
