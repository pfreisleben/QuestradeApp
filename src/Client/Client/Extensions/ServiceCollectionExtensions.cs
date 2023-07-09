using Client.Infrastructure.Managers.Identity.Authentication.Contracts;
using Client.Infrastructure.Managers.Identity.Authentication.Services;
using Client.Infrastructure.Managers.Identity.RoleClaim;
using Client.Infrastructure.Managers.Identity.Roles.Contracts;
using Client.Infrastructure.Managers.Identity.Roles.Services;
using Client.Infrastructure.Managers.Identity.Users;
using Client.Infrastructure.Managers.Score.Contracts;
using Client.Infrastructure.Managers.Score.Services;

namespace Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, AuthenticationManager>();
            services.AddTransient<IScoreManager, ScoreManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IRoleClaimManager, RoleClaimManager>();
            services.AddTransient<IUserManager, UserManager>();
            return services;
        }
    }
}
