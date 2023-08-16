using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChatApp.Test.Server.Services
{
    public class MessageServiceTests
    {
        [Fact]
        public void GetMessages_ReturnsMessages()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<Message>>();
            var testMessages = new List<Message>
            {
                new Message { ChatroomId = 1, UserId = "user1", Content = "Test message 1", Timestamp = DateTime.Now },
                new Message { ChatroomId = 1, UserId = "user2", Content = "Test message 2", Timestamp = DateTime.Now },
                new Message { ChatroomId = 2, UserId = "user3", Content = "Test message 3", Timestamp = DateTime.Now }
            }.AsQueryable();

            mockDbSet.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(testMessages.Provider);
            mockDbSet.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(testMessages.Expression);
            mockDbSet.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(testMessages.ElementType);
            mockDbSet.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(testMessages.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Messages).Returns(mockDbSet.Object);

            var messageService = new MessageService(mockContext.Object);

            // Act
            var messages = messageService.GetMessages(1);

            // Assert
            Assert.Equal(2, messages.Count);
        }

        [Fact]
        public async Task SaveMessageAsync_AddsMessageAndSavesChanges()
        {
            // Arrange
            var message = new Message { ChatroomId = 1, UserId = "user1", Content = "Test message 1", Timestamp = DateTime.Now };
            var messages = new List<Message>();
            var messageSetMock = new Mock<DbSet<Message>>();
            messageSetMock.Setup(m => m.Add(It.IsAny<Message>())).Callback<Message>(messages.Add);

            var dbContextMock = new Mock<IApplicationDbContext>();
            dbContextMock.Setup(m => m.Messages).Returns(messageSetMock.Object);
            dbContextMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);

            var service = new MessageService(dbContextMock.Object);

            // Act
            var result = await service.SaveMessageAsync(message);

            // Assert
            Assert.Equal(1, result);
            Assert.Contains(message, messages);
            dbContextMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }

}
