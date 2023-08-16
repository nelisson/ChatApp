using ChatApp.Server.Services;

namespace ChatApp.Test.Server.Services
{
    public class CommandDetectionServiceTests
    {
        [Fact]
        public void DetectCommand_ValidCommand_ReturnsCorrectCommandInfo()
        {
            // Arrange
            var commandService = new CommandDetectionService();

            // Act
            var commandInfo = commandService.DetectCommand("/stock=AAPL");

            // Assert
            Assert.True(commandInfo.IsCommand);
            Assert.Equal("AAPL", commandInfo.StockCode);
        }

        [Fact]
        public void DetectCommand_InvalidCommand_ReturnsCorrectCommandInfo()
        {
            // Arrange
            var commandService = new CommandDetectionService();

            // Act
            var commandInfo = commandService.DetectCommand("Hello, world!");

            // Assert
            Assert.False(commandInfo.IsCommand);
            Assert.Equal(string.Empty, commandInfo.StockCode);
        }
    }
}
