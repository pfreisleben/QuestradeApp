using AuthenticationApi.Application.Extensions;
using AuthenticationApi.Infrastructure.Extensions;
using AuthenticationApi.Infrastructure.Persistence.AppDb.Seed;
using AuthenticationApi.Middlewares;
using Shared.Logs.Configurations;

namespace AuthenticationApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureLayer();
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