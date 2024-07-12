using System.Net.Http.Headers;
using System.Text.Json;

namespace WVTrackerLibrary
{
    public class WVClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public WVClient(string baseUrl, List<string> endpoints)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient { BaseAddress = new Uri(_baseUrl) };
        }

        public async Task<List<ObjectiveModel>> GetObjectivesAsync(ApiKeyModel apiKey, string endpoint)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Token);

            var response = await _httpClient.GetAsync(endpoint);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException($"API key '{apiKey.Name}' is invalid or unauthorized.");
            }

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;
            var objectivesArray = root.GetProperty("objectives");

            var objectives = new List<ObjectiveModel>();
            foreach (var obj in objectivesArray.EnumerateArray())
            {
                objectives.Add(new ObjectiveModel(
                        account: apiKey.Name,
                        title: obj.GetProperty("title").GetString() ?? string.Empty,
                        track: obj.GetProperty("track").GetString() ?? string.Empty,
                        completed: obj.GetProperty("claimed").GetBoolean()
                    ));
            }

            return objectives;
        }
    }

}
