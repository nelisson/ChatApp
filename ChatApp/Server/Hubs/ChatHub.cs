using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Server.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
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

            await _messageService.SaveMessageAsync(newMessage);

            await Clients.All.SendAsync("ReceiveMessage", chatroomId);
        }
    }
}
