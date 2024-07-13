using System.Net.Http.Headers;
using System.Text.Json;

namespace WVTrackerLibrary
{
    public class WVClient
    {
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, string> _etagCache = [];
        private readonly string _cacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WVT", "Cache");
        private readonly List<string> _endpoints;


        public WVClient(string baseUrl, List<string> endpoints)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _endpoints = endpoints;
        }

        public async Task<List<ObjectiveModel>> GetObjectivesAsync(ApiKeyModel apiKey, string endpoint)
        {
            if (!_endpoints.Contains(endpoint))
            {
                throw new ArgumentException($"Invalid endpoint: {endpoint}", nameof(endpoint));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Token);

            if (_etagCache.TryGetValue($"{apiKey.Name}_{endpoint}", out string? etag) && etag != null)
            {
                _httpClient.DefaultRequestHeaders.IfNoneMatch.Clear();
                _httpClient.DefaultRequestHeaders.IfNoneMatch.Add(new EntityTagHeaderValue(etag));
            }

            var response = await _httpClient.GetAsync(endpoint);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException($"API key '{apiKey.Name}' is invalid or unauthorized.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotModified)
            {
                // Data hasn't changed, return cached data
                return await GetCachedObjectivesAsync(apiKey.Name, endpoint);
            }

            response.EnsureSuccessStatusCode();

            if (response.Headers.ETag != null)
            {
                _etagCache[$"{apiKey.Name}_{endpoint}"] = response.Headers.ETag.Tag;
            }

            var content = await response.Content.ReadAsStringAsync();
            var objectives = ParseObjectives(content, apiKey);
            await CacheObjectivesAsync(apiKey.Name, endpoint, objectives);
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
                            track: obj.GetProperty("track").GetString() ?? string.Empty,
                            completed: obj.GetProperty("claimed").GetBoolean()
                        ));
                }
                return objectives;
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error parsing objectives: {ex.Message}", ex);
            }
        }

        private async Task CacheObjectivesAsync(string apiKey, string endpoint, List<ObjectiveModel> objectives)
        {
            string fileName = GetCacheFileName(apiKey, endpoint);
            string json = JsonSerializer.Serialize(objectives);

            string? directoryName = Path.GetDirectoryName(fileName);
            if (directoryName != null)
            {
                Directory.CreateDirectory(directoryName);
            }

            await File.WriteAllTextAsync(fileName, json);
        }

        private async Task<List<ObjectiveModel>> GetCachedObjectivesAsync(string apiKey, string endpoint)
        {
            string fileName = GetCacheFileName(apiKey, endpoint);

            if (File.Exists(fileName))
            {
                string json = await File.ReadAllTextAsync(fileName);
                List<ObjectiveModel>? objectives = JsonSerializer.Deserialize<List<ObjectiveModel>>(json);
                return objectives ?? [];
            }

            return [];
        }

        private string GetCacheFileName(string apiKey, string endpoint)
        {
            return Path.Combine(_cacheDirectory, $"{apiKey}_{endpoint}.json");
        }
    }
}