using IdentityApi.Application.Extensions;
using IdentityApi.Infrastructure.Extensions;
using IdentityApi.Infrastructure.Persistence.AppDb.Seed;
using IdentityApi.Middlewares;
using Shared.Logs.Configurations;

namespace IdentityApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureLayer(configuration);
        services.AddInfrastructureMappings();
        services.AddDatabase(configuration);
        services.AddIdentityServices(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddApplicationLayer();
        services.AddStructuraLog();
        services.AddFilterToSystemLogs();
        services.RegisterSwagger();
        services.AddTransient<GlobalExceptionHandlerMiddleware>();
        services.AddTransient<UnauthorizedTokenMiddleware>();
        services.AddTransient<DataSeeder>();

        return services;
    }
}