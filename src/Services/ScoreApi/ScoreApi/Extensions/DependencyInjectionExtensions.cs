using ScoreApi.Application.Extensions;
using ScoreApi.Infrastructure.Extensions;
using ScoreApi.Middlewares;
using Shared.Logs.Configurations;

namespace ScoreApi.Extensions;

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

        return services;
    }
}