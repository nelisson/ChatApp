using ChatApp.Server.Controllers;
using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChatApp.Test.Server.Controllers
{
    public class ChatroomControllerTests
    {
        private readonly Mock<IChatroomService> _chatroomServiceMock;
        private readonly ChatroomController _controller;

        public ChatroomControllerTests()
        {
            _chatroomServiceMock = new Mock<IChatroomService>();
            _controller = new ChatroomController(_chatroomServiceMock.Object);
        }

        [Fact]
        public void GetChatrooms_ReturnsChatrooms()
        {
            // Arrange
            var testChatrooms = new List<Chatroom>
            {
                new Chatroom { Id = 1, Name = "Chatroom1" },
                new Chatroom { Id = 2, Name = "Chatroom2" }
            };
            _chatroomServiceMock.Setup(service => service.GetChatrooms()).Returns(testChatrooms);

            // Act
            var result = _controller.GetChatrooms();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Chatroom>>(okResult.Value);
            Assert.Equal(testChatrooms.Count, returnValue.Count);
        }

        [Fact]
        public async Task CreateChatroom_CreatesNewChatroom()
        {
            // Arrange
            var newChatroom = new Chatroom { Id = 3, Name = "Chatroom3" };
            _chatroomServiceMock.Setup(service => service.CreateChatroom()).ReturnsAsync(newChatroom);

            // Act
            var result = await _controller.CreateChatroom();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Chatroom>(createdAtActionResult.Value);
            Assert.Equal(newChatroom.Id, returnValue.Id);
            Assert.Equal(newChatroom.Name, returnValue.Name);
        }
    }

}
