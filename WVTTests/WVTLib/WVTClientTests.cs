using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using WVTLib;
using WVTLib.Models;
using Serilog;

namespace TestWVT.WVTLib
{
    public class WVTClientTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly WVTClient _client;
        private readonly Mock<ILogger> _mockLogger;
        private readonly TimeSpan _testTimeout = TimeSpan.FromSeconds(10);

        public WVTClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockLogger = new Mock<ILogger>();
            _mockLogger.Setup(x => x.ForContext<It.IsAnyType>()).Returns(_mockLogger.Object);
            _client = new WVTClient(_httpClient, "https://api.example.com/v2/account/wizardsvault/",
                new List<string> { "daily", "weekly", "special" }, _mockLogger.Object, _testTimeout);
        }

        [Fact]
        public async Task GetObjectivesAsync_ValidEndpoint_ReturnsObjectives()
        {
            // Arrange
            var apiKey = new ApiKeyModel("TestKey", "test-token");
            var jsonResponse = @"{
                ""objectives"": [
                    {""title"": ""Objective 1"", ""track"": ""Track 1""},
                    {""title"": ""Objective 2"", ""track"": ""Track 2""}
                ]
            }";
            SetupMockHttpMessageHandler(HttpStatusCode.OK, jsonResponse);

            // Act
            var result = await _client.GetObjectivesAsync(apiKey, "daily", CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result[0].Title.Should().Be("Objective 1");
            result[0].Track.Should().Be("Track 1");
            result[1].Title.Should().Be("Objective 2");
            result[1].Track.Should().Be("Track 2");
        }

        [Fact]
        public async Task GetObjectivesAsync_InvalidEndpoint_ThrowsArgumentException()
        {
            // Arrange
            var apiKey = new ApiKeyModel("TestKey", "test-token");

            // Act & Assert
            await _client.Invoking(c => c.GetObjectivesAsync(apiKey, "invalid", CancellationToken.None))
                .Should().ThrowAsync<ArgumentException>()
                .WithMessage("Invalid endpoint: invalid*");
        }

        [Fact]
        public async Task GetObjectivesAsync_UnauthorizedApiKey_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var apiKey = new ApiKeyModel("TestKey", "invalid-token");
            SetupMockHttpMessageHandler(HttpStatusCode.Unauthorized, "");

            // Act & Assert
            await _client.Invoking(c => c.GetObjectivesAsync(apiKey, "daily", CancellationToken.None))
                .Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("API key 'TestKey' is invalid or unauthorized.");
        }

        [Fact]
        public async Task GetObjectivesAsync_TransientError_RetriesToSuccess()
        {
            // Arrange
            var apiKey = new ApiKeyModel("TestKey", "test-token");
            var jsonResponse = @"{
                ""objectives"": [
                    {""title"": ""Objective 1"", ""track"": ""Track 1""}
                ]
            }";

            var callCount = 0;
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(() => {
                    callCount++;
                    if (callCount < 3)
                    {
                        return new HttpResponseMessage(HttpStatusCode.RequestTimeout);
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonResponse) };
                });

            // Act
            var result = await _client.GetObjectivesAsync(apiKey, "daily", CancellationToken.None);

            // Assert
            result.Should().HaveCount(1);
            result[0].Title.Should().Be("Objective 1");
            result[0].Track.Should().Be("Track 1");

            _mockLogger.Verify(
                x => x.Warning(
                    It.Is<string>(s => s.Contains("Request failed")),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<int>()
                ),
                Times.Exactly(2)
            );

            callCount.Should().Be(3);
        }

        [Fact]
        public async Task GetObjectivesAsync_Cancellation_ThrowsOperationCanceledException()
        {
            // Arrange
            var apiKey = new ApiKeyModel("TestKey", "test-token");
            var cts = new CancellationTokenSource();

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(() => {
                    cts.Cancel();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                });

            // Act & Assert
            await _client.Invoking(c => c.GetObjectivesAsync(apiKey, "daily", cts.Token))
                .Should().ThrowAsync<OperationCanceledException>();
        }
        [Fact]
        public async Task GetObjectivesAsync_Timeout_ThrowsTimeoutException()
        {
            // Arrange
            var apiKey = new ApiKeyModel("TestKey", "test-token");

            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns<HttpRequestMessage, CancellationToken>((request, cancellationToken) =>
                {
                    // Create a task that will not complete within the timeout period
                    return Task.Delay(Timeout.Infinite, cancellationToken)
                        .ContinueWith(t => new HttpResponseMessage(HttpStatusCode.OK), TaskContinuationOptions.OnlyOnCanceled);
                });

            // Act & Assert
            await _client.Invoking(c => c.GetObjectivesAsync(apiKey, "daily", CancellationToken.None))
                .Should().ThrowAsync<TimeoutException>();
        }

        private void SetupMockHttpMessageHandler(HttpStatusCode statusCode, string content)
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(content)
                });
        }
    }
}