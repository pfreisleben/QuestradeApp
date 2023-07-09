using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Client.Infrastructure.AuthenticationHandlers;

public class IdentityApiAuthenticationHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigationManager;


    public IdentityApiAuthenticationHandler(ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
    {
        _localStorage = localStorage;
        _authenticationStateProvider = authenticationStateProvider;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        var savedToken = await _localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrWhiteSpace(savedToken) || JwtIsExpired(savedToken))
        {
            await _localStorage.RemoveItemAsync("authToken");
            request.Headers.Authorization = null;
            _navigationManager.NavigateTo("/logout");

        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

        return await base.SendAsync(request, cancellationToken);
    }

    private bool JwtIsExpired(string jwt)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(jwt);
        var expirationTime = jwtToken.ValidTo;

        return expirationTime < DateTime.UtcNow;
    }
}