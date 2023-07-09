using ScoreApi.Application.Contracts.Services;
using ScoreApi.Infrastructure.Identity.Services;
using Shared.Constants;

namespace ScoreApi.Extensions;

public static class Identity
{
    /// <summary>
    /// Adiciona ao container serviços relacionados a identidade do usuário
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {

            foreach (var permission in Permissions.GetRegisteredPermissions())
            {
                options.AddPolicy(permission, policy => policy.RequireClaim(ApplicationClaimTypes.Permission, permission));
            }

        });

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();


        return services;
    }
}