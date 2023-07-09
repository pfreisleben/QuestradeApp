using System.Net.Http.Json;
using Client.Infrastructure.Managers.Score.Contracts;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests.Score;

namespace Client.Infrastructure.Managers.Score.Services;

public class ScoreManager : IScoreManager
{
    private readonly HttpClient _httpClient;

    public ScoreManager(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.ScoreApi);
    }

    public async Task<CommandResult<ScoreResponse>> GetScoreByUserId(string userId)
    {
        var endpoint = $"score/GetUserScore/{userId}";
        var response = await _httpClient.GetAsync(endpoint);
        try
        {
            var result = await response.Content.ReadFromJsonAsync<CommandResult<ScoreResponse>>();
            return result;
        }
        catch (Exception e)
        {
            return await CommandResult<ScoreResponse>.FailAsync(
                $"Falha ao buscar score: {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async Task<CommandResult<bool>> CheckIfUserCanApplyToLoan(string userId)
    {
        var endpoint = $"score/CheckIfCanApplyToLoan/{userId}";
        var response = await _httpClient.GetAsync(endpoint);
        try
        {
            var result = await response.Content.ReadFromJsonAsync<CommandResult<bool>>();
            return result;
        }
        catch (Exception e)
        {
            return await CommandResult<bool>.FailAsync(
                $"Falha ao buscar score: {await response.Content.ReadAsStringAsync()}");
        }
        
    }
}