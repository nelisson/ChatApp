using ChatApp.Server.Services;
using ChatApp.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChatApp.Test.Server.Services
{
    public class ChatroomServiceTests
    {
        private readonly Mock<IApplicationDbContext> _dbContextMock;
        private readonly ChatroomService _chatroomService;
        private readonly List<Chatroom> _testChatrooms;

        public ChatroomServiceTests()
        {
            _testChatrooms = new List<Chatroom>
            {
                new Chatroom { Name = "Chatroom_1" },
                new Chatroom { Name = "Chatroom_2" },
                new Chatroom { Name = "Chatroom_3" }
            };

            var mockDbSet = new Mock<DbSet<Chatroom>>();
            var queryableTestChatrooms = _testChatrooms.AsQueryable();

            mockDbSet.As<IQueryable<Chatroom>>().Setup(m => m.Provider).Returns(queryableTestChatrooms.Provider);
            mockDbSet.As<IQueryable<Chatroom>>().Setup(m => m.Expression).Returns(queryableTestChatrooms.Expression);
            mockDbSet.As<IQueryable<Chatroom>>().Setup(m => m.ElementType).Returns(queryableTestChatrooms.ElementType);
            mockDbSet.As<IQueryable<Chatroom>>().Setup(m => m.GetEnumerator()).Returns(queryableTestChatrooms.GetEnumerator());

            _dbContextMock = new Mock<IApplicationDbContext>();
            _dbContextMock.Setup(c => c.Chatrooms).Returns(mockDbSet.Object);

            _chatroomService = new ChatroomService(_dbContextMock.Object);
        }

        [Fact]
        public void GetChatrooms_ReturnsChatrooms()
        {
            // Act
            var chatrooms = _chatroomService.GetChatrooms();

            // Assert
            Assert.Equal(3, chatrooms.Count());
        }

        [Fact]
        public async Task CreateChatroom_AddsChatroomAndSavesChanges()
        {
            // Act
            var chatroom = await _chatroomService.CreateChatroom();

            // Assert
            _dbContextMock.Verify(db => db.Chatrooms.Add(It.IsAny<Chatroom>()), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(), Times.Once);
            Assert.NotNull(chatroom);
            Assert.StartsWith("Chatroom_", chatroom.Name);
        }
    }
}
