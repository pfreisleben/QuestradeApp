using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using Client.Infrastructure.Extensions;
using Client.Infrastructure.Managers.Identity.Authentication.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests;

namespace Client.Infrastructure.Managers.Identity.Authentication.Services;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _httpClientWithoutAuthentication;

    public AuthenticationManager(IHttpClientFactory httpClientFactory,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.IdentityApi);
        _httpClientWithoutAuthentication = httpClientFactory.CreateClient(HttpClientNames.IdentityApiWithoutAuthentication);
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<CommandResult> Register(RegisterRequest registerModel)
    {
        var result = await _httpClientWithoutAuthentication.PostAsJsonAsync("api/identity/account/register", registerModel);

        try
        {
            var commandResult = await result.Content.ReadFromJsonAsync<CommandResult>();
            return commandResult!;
        }
        catch (Exception)
        {
            return (CommandResult)await CommandResult.FailAsync(
                $"Algum erro ocorreu ao realizar o cadastro. StatusCode: {result.StatusCode}");
        }

    }

    public async Task<ICommandResult> Login(LoginRequest loginModel)
    {

        var response = await _httpClientWithoutAuthentication.PostAsJsonAsync("api/identity/account/login", loginModel);

        if (response.IsSuccessStatusCode)
        {
            var tokenDto = await response.ToResult<string>();
            await _localStorage.SetItemAsync("authToken", tokenDto!.Data);
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).StateChangedAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenDto!.Data);
            return (CommandResult)await CommandResult.SuccessAsync("Login realizado com sucesso.");
        }

        return await response.ToResult();

    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<ClaimsPrincipal> CurrentUser()
    {
        var state = await ((ApiAuthenticationStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();
        return state.User;
    }
}