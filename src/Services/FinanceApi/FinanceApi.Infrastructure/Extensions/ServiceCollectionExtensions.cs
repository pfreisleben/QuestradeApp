using System.Reflection;
using FinanceApi.Application.Bills.Contracts;
using FinanceApi.Application.Loans.Contracts;
using FinanceApi.Application.Scores;
using FinanceApi.Infrastructure.Bills;
using FinanceApi.Infrastructure.Loans;
using FinanceApi.Infrastructure.Scores;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IScoreService, ScoreService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IBillService, BillService>();
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
