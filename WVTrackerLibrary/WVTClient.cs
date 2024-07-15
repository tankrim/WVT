using System.Net.Http.Headers;
using System.Text.Json;
using WVTLib.Models;

namespace WVTLib
{
    public class WVTClient
    {
        private readonly HttpClient _httpClient;
        private readonly List<string> _endpoints;

        public WVTClient(HttpClient httpClient, string baseUrl, List<string> endpoints)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
            _endpoints = endpoints;
        }

        public async Task<List<ObjectiveModel>> GetObjectivesAsync(ApiKeyModel apiKey, string endpoint)
        {
            if (!_endpoints.Contains(endpoint))
            {
                throw new ArgumentException($"Invalid endpoint: {endpoint}", nameof(endpoint));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Token);
            var response = await _httpClient.GetAsync(endpoint);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException($"API key '{apiKey.Name}' is invalid or unauthorized.");
            }

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var objectives = ParseObjectives(content, apiKey);
            return objectives;
        }

        private static List<ObjectiveModel> ParseObjectives(string content, ApiKeyModel apiKey)
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
                throw new JsonException($"Error parsing objectives: {ex.Message}", ex);
            }
        }
    }
}