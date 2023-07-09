using FinanceApi.Application.Extensions;
using FinanceApi.Infrastructure.Extensions;
using FinanceApi.Middlewares;
using Shared.Constants;
using Shared.Logs.Configurations;

namespace FinanceApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(HttpClientNames.ScoreApi,
            client => client.BaseAddress =
                new Uri(configuration.GetValue<string>("ScoreApiUrl")));
        
        
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