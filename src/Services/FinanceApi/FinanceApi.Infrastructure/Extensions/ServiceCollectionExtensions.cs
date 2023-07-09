using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructureLayer(this IServiceCollection services)
    {

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
