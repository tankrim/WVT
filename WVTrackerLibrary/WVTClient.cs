using Polly;
using Polly.CircuitBreaker;
using Polly.Wrap;
using Polly.Retry;
using Serilog;
using System.Net;
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
        private readonly AsyncPolicyWrap<HttpResponseMessage> _policyWrap;

        public WVTClient(HttpClient httpClient, string baseUrl, List<string> endpoints, ILogger logger, TimeSpan requestTimeout)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.Timeout = requestTimeout;
            _endpoints = endpoints;
            _logger = logger.ForContext<WVTClient>();

            var circuitBreakerPolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(msg => (int)msg.StatusCode >= 500 || msg.StatusCode == HttpStatusCode.RequestTimeout)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromMinutes(1),
                    onBreak: (outcome, breakDelay) =>
                    {
                        _logger.Warning("Circuit breaker opened for {BreakDelay}", breakDelay);
                    },
                    onReset: () =>
                    {
                        _logger.Information("Circuit breaker reset");
                    },
                    onHalfOpen: () =>
                    {
                        _logger.Information("Circuit breaker half-open");
                    });

            var retryPolicy = Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(msg => (int)msg.StatusCode >= 500 || msg.StatusCode == HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(
                    3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (outcome, timespan, retryCount, context) =>
                    {
                        _logger.Warning("Request failed. Waiting {TimeSpan} before retry. Retry attempt {RetryCount}", timespan, retryCount);
                    }
                );

            _policyWrap = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy);
        }

        public async Task<List<ObjectiveModel>> GetObjectivesAsync(ApiKeyModel apiKey, string endpoint, CancellationToken cancellationToken = default)
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
                var response = await _policyWrap.ExecuteAsync(async (ct) =>
                    await _httpClient.GetAsync(endpoint, ct), cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.Warning("Unauthorized access attempt for API key {KeyName}", apiKey.Name);
                    throw new UnauthorizedAccessException($"API key '{apiKey.Name}' is invalid or unauthorized.");
                }

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(CancellationToken.None);
                var objectives = ParseObjectives(content, apiKey);

                _logger.Information("Successfully fetched {Count} objectives for endpoint {Endpoint}", objectives.Count, endpoint);

                return objectives;
            }
            catch (TaskCanceledException ex)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.Information("Operation was canceled for endpoint {Endpoint}", endpoint);
                    throw new OperationCanceledException("The operation was canceled.", ex, cancellationToken);
                }
                else
                {
                    _logger.Warning("Operation timed out for endpoint {Endpoint}", endpoint);
                    throw new TimeoutException("The operation timed out.", ex);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Information("Objective fetching cancelled for endpoint {Endpoint}", endpoint);
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.Error(ex, "HTTP request failed for endpoint {Endpoint} after retries", endpoint);
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