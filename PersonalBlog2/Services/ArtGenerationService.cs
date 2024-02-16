using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class ArtGenerationService : BackgroundService 
{
    
    private readonly HttpClient _httpClient;

    public ArtGenerationService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    private async Task GenerateAiArtAsync()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "URL");  
            request.Headers.Add("Authorization", "Bearer API_KEY");
            // if API expects a JSON payload
            var payload = new {/* TODO: JSON payload */};
            
            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, 
                "application/json");
            
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                // TODO: Save the generated art to an image file
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            GenerateAiArt();

            // Wait defined period before generating next art
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    private void GenerateAiArt()
    {
        Console.WriteLine("Generating AI art...");
        // TODO: Implement logic to call art generation API
    }
}