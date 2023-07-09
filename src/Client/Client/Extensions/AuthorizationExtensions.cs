using Microsoft.AspNetCore.Authorization;
using Shared.Constants;

namespace Client.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddClientAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationCore(opt => { RegisterPermissionClaims(opt); });
            return services;
        }

        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            foreach (var permission in Permissions.GetRegisteredPermissions())
            {
                options.AddPolicy(permission, policy => policy.RequireClaim(ApplicationClaimTypes.Permission, permission));

            }

        }

    }
}
