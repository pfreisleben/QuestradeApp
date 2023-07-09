using FinanceApi.Infrastructure.Persistence.AppDb;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Extensions;

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