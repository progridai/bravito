using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;
using Bravito.Infrastructure.Knowledge.Options;
using Microsoft.Extensions.Options;

namespace Bravito.Infrastructure.Embeddings;

public class GeminiEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;

    public GeminiEmbeddingService(HttpClient httpClient, IOptions<GeminiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
            throw new InvalidOperationException("Gemini API Key is not configured.");

        var model = _options.EmbeddingModel;
        if (string.IsNullOrWhiteSpace(model))
            model = "models/text-embedding-004";

        var url = $"https://generativelanguage.googleapis.com/v1beta/{model}:embedContent?key={_options.ApiKey}";

        var payload = new
        {
            model = model,
            content = new
            {
                parts = new[]
                {
                    new { text = text }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(url, payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Failed to generate embedding from Gemini API. Status: {response.StatusCode}. Details: {errorContent}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(jsonResponse);

        var valuesElement = doc.RootElement.GetProperty("embedding").GetProperty("values");
        
        var values = new float[valuesElement.GetArrayLength()];
        int i = 0;
        foreach (var element in valuesElement.EnumerateArray())
        {
            values[i++] = element.GetSingle();
        }

        return values;
    }
}
