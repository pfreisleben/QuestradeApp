using System.Net.Http.Json;
using IdentityApi.Domain.DomainEvents;
using MediatR;
using Shared.Constants;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests.Score;

namespace IdentityApi.Application.Handlers;

public class UserCreatedHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly HttpClient _httpClient;
    private readonly ILogServices _logServices;

    public UserCreatedHandler(IHttpClientFactory httpClientFactory, ILogServices logServices)
    {
        _logServices = logServices;
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.ScoreApi);
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var endpoint = $"score/InitializeUserScore";
        var request = new InitializeUserScoreRequest(notification.UserId);
        
        var response = await _httpClient.PostAsJsonAsync(endpoint, request);
        var result = await response.Content.ReadFromJsonAsync<CommandResult>();
        
        if (result.Succeeded)
        {
            _logServices.WriteMessage($"User score initialized successfully!");
        }
    }
}