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

builder.Services.AddHttpClient(HttpClientNames.AuthenticationApiWithoutAuthentication,
    client => client.BaseAddress =
        new Uri(builder.Configuration.GetValue<string>("ApiEndpoints:AuthenticationApi")));

builder.Services.AddHttpClient(HttpClientNames.AuthenticationApi,
    client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiEndpoints:AuthenticationApi")))
    .AddHttpMessageHandler<ApiAuthenticationHandler>();



builder.Services.AddManagers();
builder.Services.AddClientAuthorization();
builder.Services.AddTransient<ApiAuthenticationHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

await builder.Build().RunAsync();

