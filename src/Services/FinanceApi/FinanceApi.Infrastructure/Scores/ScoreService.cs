using System.Net.Http.Json;
using FinanceApi.Application.Scores;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests.Score;

namespace FinanceApi.Infrastructure.Scores;

public class ScoreService : IScoreService
{
    private readonly HttpClient _httpClient;

    public ScoreService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.ScoreApi);
    }

    public async Task<CommandResult> AddScore(AddScoreRequest request)
    {
        var endpoint = $"score/AddScore";
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);
        var result = await response.Content.ReadFromJsonAsync<CommandResult>();
        return result!;
    }
}