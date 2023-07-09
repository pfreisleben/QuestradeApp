using Microsoft.EntityFrameworkCore;
using ScoreApi.Infrastructure.Persistence.AppDb;

namespace ScoreApi.Extensions;

public static class ApplicationBuilderExtensions
{
    internal static async Task<IApplicationBuilder> InitializeDatabase(this IApplicationBuilder app)
    {

        using var serviceScope = app.ApplicationServices.CreateScope();
        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


        await appDbContext.Database.MigrateAsync();

        return await Task.FromResult(app);
    }
}