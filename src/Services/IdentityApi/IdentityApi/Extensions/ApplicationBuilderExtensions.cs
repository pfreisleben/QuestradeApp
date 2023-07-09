using IdentityApi.Infrastructure.Persistence.AppDb;
using IdentityApi.Infrastructure.Persistence.AppDb.Seed;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Extensions;

public static class ApplicationBuilderExtensions
{
    internal static async Task<IApplicationBuilder> InitializeDatabase(this IApplicationBuilder app)
    {

        using var serviceScope = app.ApplicationServices.CreateScope();
        var initalizer = serviceScope.ServiceProvider.GetRequiredService<DataSeeder>();
        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


        await appDbContext.Database.MigrateAsync();
        await initalizer.Initialize();

        return await Task.FromResult(app);
    }
}