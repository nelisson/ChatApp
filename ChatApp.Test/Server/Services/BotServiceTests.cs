using ChatApp.Server.Services;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;

namespace ChatApp.Test.Server.Services
{
    public class BotServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly BotService _botService;

        public BotServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _botService = new BotService(_httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task ProcessStockCommand_ShouldReturnFormattedMessage_WhenFetchStockDataSucceeds()
        {
            // Arrange
            var stockCode = "AAPL";
            var responseContent =
@"Symbol,Date,Time,Open,High,Low,Close,Volume
AAPL,2023-08-15,16:00:00,149.99,150.44,149.49,150.03,58383274";

            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            var httpClient = new HttpClient(messageHandler.Object);
            _httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _botService.ProcessStockCommand(stockCode);

            // Assert
            result.Should().Be("AAPL quote is $150.03 per share.");
        }

        [Fact]
        public async Task ProcessStockCommand_ShouldReturnErrorMessage_WhenFetchStockDataFails()
        {
            // Arrange
            var stockCode = "AAPL";
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                });

            var httpClient = new HttpClient(messageHandler.Object);
            _httpClientFactoryMock.Setup(factory => factory.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _botService.ProcessStockCommand(stockCode);

            // Assert
            result.Should().Be("Error fetching stock data.");
        }
    }
}
