using System.Reflection;
using IdentityApi.Application.Services.Identity.Contracts;
using IdentityApi.Infrastructure.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;

namespace IdentityApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRoleClaimService, RoleClaimService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddHttpClient(HttpClientNames.ScoreApi,
            client => client.BaseAddress = new Uri(configuration.GetValue<string>("ScoreApiUrl")!));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    public static void AddInfrastructureMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
