using Serilog;
using System.Net.Http.Headers;
using System.Text.Json;
using WVTLib.Models;

namespace WVTLib
{
    public class WVTClient
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _endpoints;
        private readonly ILogger _logger;

        public WVTClient(HttpClient httpClient, string baseUrl, List<string> endpoints)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
            _endpoints = endpoints;
            _logger = Log.ForContext<WVTClient>();
        }

        public async Task<List<ObjectiveModel>> GetObjectivesAsync(ApiKeyModel apiKey, string endpoint)
        {
            _logger.Debug("Fetching objectives for endpoint {Endpoint} with API key {KeyName}", endpoint, apiKey.Name);

            if (!_endpoints.Contains(endpoint))
            {
                _logger.Warning("Invalid endpoint {Endpoint} requested", endpoint);
                throw new ArgumentException($"Invalid endpoint: {endpoint}", nameof(endpoint));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Token);

            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _logger.Warning("Unauthorized access attempt for API key {KeyName}", apiKey.Name);
                    throw new UnauthorizedAccessException($"API key '{apiKey.Name}' is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var objectives = ParseObjectives(content, apiKey);

                _logger.Information("Successfully fetched {Count} objectives for endpoint {Endpoint}", objectives.Count, endpoint);

                return objectives;
            }
            catch (HttpRequestException ex)
            {
                _logger.Error(ex, "HTTP request failed for endpoint {Endpoint}", endpoint);
                throw;
            }
            catch (Exception ex) when (ex is not UnauthorizedAccessException)
            {
                _logger.Error(ex, "Unexpected error occurred while fetching objectives for endpoint {Endpoint}", endpoint);
                throw;
            }
        }

        private List<ObjectiveModel> ParseObjectives(string content, ApiKeyModel apiKey)
        {
            try
            {
                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;
                var objectivesArray = root.GetProperty("objectives");
                var objectives = new List<ObjectiveModel>();
                foreach (var obj in objectivesArray.EnumerateArray())
                {
                    objectives.Add(new ObjectiveModel(
                            account: apiKey.Name,
                            title: obj.GetProperty("title").GetString() ?? string.Empty,
                            track: obj.GetProperty("track").GetString() ?? string.Empty
                        ));
                }
                return objectives;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error parsing objectives JSON");
                throw new JsonException($"Error parsing objectives: {ex.Message}", ex);
            }
        }
    }
}