using Microsoft.EntityFrameworkCore;
using ScoreApi.Infrastructure.Persistence.AppDb;

namespace ScoreApi.Extensions;

public static class Persistence
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var appDbConnectionString = configuration.GetConnectionString("AppConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(appDbConnectionString));
        


        return services;
    }
}