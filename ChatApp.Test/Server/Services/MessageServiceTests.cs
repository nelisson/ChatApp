using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChatApp.Test.Server.Services
{
    public class MessageServiceTests
    {
        private readonly Mock<IApplicationDbContext> _dbContextMock;
        private readonly MessageService _messageService;
        private readonly List<Message> _testMessages;

        public MessageServiceTests()
        {
            _testMessages = new List<Message>
            {
                new Message { ChatroomId = 1, UserId = "user1", Content = "Test message 1", Timestamp = DateTime.Now },
                new Message { ChatroomId = 1, UserId = "user2", Content = "Test message 2", Timestamp = DateTime.Now },
                new Message { ChatroomId = 2, UserId = "user3", Content = "Test message 3", Timestamp = DateTime.Now }
            };

            var mockDbSet = new Mock<DbSet<Message>>();
            var queryableTestMessages = _testMessages.AsQueryable();

            mockDbSet.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(queryableTestMessages.Provider);
            mockDbSet.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(queryableTestMessages.Expression);
            mockDbSet.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(queryableTestMessages.ElementType);
            mockDbSet.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(queryableTestMessages.GetEnumerator());

            _dbContextMock = new Mock<IApplicationDbContext>();
            _dbContextMock.Setup(c => c.Messages).Returns(mockDbSet.Object);
            _dbContextMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);


            _messageService = new MessageService(_dbContextMock.Object);
        }

        [Fact]
        public void GetMessages_ReturnsMessages()
        {
            // Act
            var messages = _messageService.GetMessages(1);

            // Assert
            Assert.Equal(2, messages.Count);
        }

        [Fact]
        public async Task SaveMessageAsync_AddsMessageAndSavesChanges()
        {
            // Arrange
            var newMessage = new Message { ChatroomId = 1, UserId = "user4", Content = "New test message", Timestamp = DateTime.Now };

            // Act
            var result = await _messageService.SaveMessageAsync(newMessage);

            // Assert
            _dbContextMock.Verify(db => db.Messages.Add(It.IsAny<Message>()), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(), Times.Once);
            Assert.Equal(1, result);
        }
    }

}
