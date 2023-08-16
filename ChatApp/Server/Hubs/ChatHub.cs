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
        private readonly IBotService _botService;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHub(IMessageService messageService, ICommandDetectionService commandDetectionService, IBotService botService, IRabbitMqService rabbitMqService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _commandDetectionService = commandDetectionService;
            _botService = botService;
            _rabbitMqService = rabbitMqService;
            _hubContext = hubContext;

            string rabbitMqQueue = "stockMessages";
            _rabbitMqService.RegisterConsumer(rabbitMqQueue, ProcessReceivedMessage);
        }

        private async void ProcessReceivedMessage(string message)
        {
            var splitMessage = message.Split("@@@");
            int chatroomId = int.Parse(splitMessage[0]);
            string stockMessage = splitMessage[1];

            // Processar e enviar a mensagem para o cliente usando o método SendAsync
            await _hubContext.Clients.All.SendAsync("ReceiveStockMessage", chatroomId, $"<i>{DateTime.Now}</i> <strong>Stock Bot</strong>: {stockMessage}");
        }


        public async Task SendMessage(int chatroomId, string userId, string message)
        {
            var commando = _commandDetectionService.DetectCommand(message);

            if (commando.IsCommand)
            {
                var stockMessage = await _botService.ProcessStockCommand(commando.StockCode);

                _botService.SendMessageToBroker(stockMessage, chatroomId);
            }
            else
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
