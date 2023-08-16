using ChatApp.Server.Hubs;
using ChatApp.Server.Models;
using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.SignalR;
using Moq;

namespace ChatApp.Test.Server.Hubs
{
    public class ChatHubTests
    {
        private readonly ChatHub _chatHub;
        private readonly Mock<IMessageService> _messageServiceMock;
        private readonly Mock<ICommandDetectionService> _commandDetectionServiceMock;
        private readonly Mock<IHubCallerClients> _clientsMock;
        private readonly Mock<IClientProxy> _clientProxyMock;

        public ChatHubTests()
        {
            _messageServiceMock = new Mock<IMessageService>();
            _clientsMock = new Mock<IHubCallerClients>();
            _clientProxyMock = new Mock<IClientProxy>();
            _commandDetectionServiceMock = new Mock<ICommandDetectionService>();

            _clientsMock.Setup(m => m.All).Returns(_clientProxyMock.Object);
            _clientProxyMock.Setup(m => m.SendCoreAsync("ReceiveMessage", It.IsAny<object[]>(), default)).Returns(Task.CompletedTask);

            _chatHub = new ChatHub(_messageServiceMock.Object, _commandDetectionServiceMock.Object) { Clients = _clientsMock.Object };
        }

        [Fact]
        public async Task SendMessage_Saves_Message()
        {
            // Arrange
            var chatroomId = 1;
            var userId = "user1";
            var content = "Test message";
            var newMessage = new Message
            {
                ChatroomId = chatroomId,
                UserId = userId,
                Content = content,
                Timestamp = DateTime.Now
            };

            var command = new CommandInfo
            { 
                IsCommand = false,
                StockCode = string.Empty
            };

            _messageServiceMock.Setup(service => service.SaveMessageAsync(It.IsAny<Message>()))
                                .ReturnsAsync(1)
                                .Verifiable();
            _commandDetectionServiceMock.Setup(service => service.DetectCommand(It.IsAny<string>()))
                                .Returns(command);

            // Act
            await _chatHub.SendMessage(chatroomId, userId, content);

            // Assert
            _messageServiceMock.Verify(service => service.SaveMessageAsync(It.IsAny<Message>()), Times.Once);
            _clientProxyMock.Verify(m => m.SendCoreAsync("ReceiveMessage", It.Is<object[]>(o => (int)o[0] == chatroomId), default), Times.Once);
        }
    }
}
