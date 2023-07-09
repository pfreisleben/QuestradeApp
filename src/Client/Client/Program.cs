using Blazored.LocalStorage;
using Client;
using Client.Extensions;
using Client.Infrastructure.AuthenticationHandlers;
using Client.Infrastructure.Managers.Identity.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Shared.Constants;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
    config.SnackbarConfiguration.PreventDuplicates = true;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 8000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddHttpClient(HttpClientNames.IdentityApiWithoutAuthentication,
    client => client.BaseAddress =
        new Uri(builder.Configuration.GetValue<string>("ApiEndpoints:IdentityApi")));

builder.Services.AddHttpClient(HttpClientNames.ScoreApi,
    client => client.BaseAddress =
        new Uri(builder.Configuration.GetValue<string>("ApiEndpoints:ScoreApi")));

builder.Services.AddHttpClient(HttpClientNames.FinanceApi,
    client => client.BaseAddress =
        new Uri(builder.Configuration.GetValue<string>("ApiEndpoints:FinanceApi")));

builder.Services.AddHttpClient(HttpClientNames.IdentityApi,
    client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiEndpoints:IdentityApi")))
    .AddHttpMessageHandler<IdentityApiAuthenticationHandler>();



builder.Services.AddManagers();
builder.Services.AddClientAuthorization();
builder.Services.AddTransient<IdentityApiAuthenticationHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

await builder.Build().RunAsync();

