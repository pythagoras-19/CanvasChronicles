/*
 * DEPRECATED: This service is currently not in use.
 */
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class ArtGenerationService : BackgroundService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ArtGenerationService> _logger;
    private readonly string _apiKey;

    public ArtGenerationService(IHttpClientFactory httpClientFactory, 
                                IConfiguration configuration, 
                                ILogger<ArtGenerationService> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
        _apiKey = configuration["OPENAI_API_KEY"];
    }

    private async Task GenerateAiArtAsync()
    {
        try
        {
            var requestUrl = "https://api.openai.com/v1/images/generations";
            var payload = new
            {
                prompt = "An extremely cute black cat.",
                n = 1 
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            
            var response = await _httpClient.PostAsync(requestUrl, requestContent);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("AI art generated successfully!");
                // TODO: Process the response, such as saving the generated art
            }
            else
            {
                _logger.LogError($"Error generating AI art: {response.StatusCode} - {response.ReasonPhrase}");
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error response from API: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occurred while generating AI art: {ex.Message}");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ArtGenerationService running.");
        while (!stoppingToken.IsCancellationRequested)
        {
            await GenerateAiArtAsync();

            // wait
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
