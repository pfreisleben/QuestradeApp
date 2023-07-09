using System.Reflection;
using IdentityApi.Application.Services.Identity.Contracts;
using IdentityApi.Infrastructure.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IRoleClaimService, RoleClaimService>();
        services.AddScoped<IRoleService, RoleService>();

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
