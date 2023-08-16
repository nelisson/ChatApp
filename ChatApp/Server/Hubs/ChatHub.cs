using ChatApp.Server.Models;
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
        private readonly ICommandDetectionService _commandDetectionService;

        public ChatHub(IMessageService messageService, ICommandDetectionService commandDetectionService)
        {
            _messageService = messageService;
            _commandDetectionService = commandDetectionService;
        }

        
        public async Task SendMessage(int chatroomId, string userId, string message)
        {
            var commando = _commandDetectionService.DetectCommand(message);

            if(commando.IsCommand)
            {
                // call botservice
            }else
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
}
