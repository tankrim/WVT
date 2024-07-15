using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using WVTLib;
using WVTLib.Models;

namespace TestWVT.WVTLib
{
    public class WVTClientTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly WVTClient _client;

        public WVTClientTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _client = new WVTClient(_httpClient, "https://api.example.com/v2/account/wizardsvault/", ["daily", "weekly", "special"]);
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
            var result = await _client.GetObjectivesAsync(apiKey, "daily");

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
            await _client.Invoking(c => c.GetObjectivesAsync(apiKey, "invalid"))
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
            await _client.Invoking(c => c.GetObjectivesAsync(apiKey, "daily"))
                .Should().ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("API key 'TestKey' is invalid or unauthorized.");
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
