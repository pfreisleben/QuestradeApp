using AuthenticationApi.Application.Contracts.Services;
using AuthenticationApi.Application.Services.Identity.Contracts;
using AuthenticationApi.Domain.Entites;
using AuthenticationApi.Infrastructure.Identity.Services;
using AuthenticationApi.Infrastructure.Persistence.AppDb;
using Microsoft.AspNetCore.Identity;
using Shared.Constants;

namespace AuthenticationApi.Extensions;

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

        services.AddIdentityCore<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = false;
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IAccountService, AccountService>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserService, UserService>();


        return services;
    }
}