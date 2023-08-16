using ChatApp.Server.Controllers;
using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChatApp.Test.Server.Controllers
{
    public class MessageControllerTests
    {
        private readonly MessageController _controller;
        private readonly Mock<IMessageService> _messageServiceMock;

        public MessageControllerTests()
        {
            _messageServiceMock = new Mock<IMessageService>();
            _controller = new MessageController(_messageServiceMock.Object);
        }

        [Fact]
        public void List_ReturnsMessages()
        {
            // Arrange
            var chatroomId = 1;
            var testMessages = new List<Message>
            {
                new Message { ChatroomId = chatroomId, UserId = "user1", Content = "Test message 1", Timestamp = DateTime.Now },
                new Message { ChatroomId = chatroomId, UserId = "user2", Content = "Test message 2", Timestamp = DateTime.Now }
            };

            _messageServiceMock.Setup(service => service.GetMessages(chatroomId)).Returns(testMessages);

            // Act
            var result = _controller.List(chatroomId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var messages = Assert.IsType<List<Message>>(okResult.Value);
            Assert.Equal(2, messages.Count);
            Assert.Equal(testMessages, messages);
        }
    }
}
