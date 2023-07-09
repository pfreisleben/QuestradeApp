using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ScoreApi.Application.Occurrences.Contracts;
using ScoreApi.Application.Score.Contracts;
using ScoreApi.Infrastructure.Occurrences;
using ScoreApi.Infrastructure.Scores;

namespace ScoreApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{

    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IOccurrenceService, OccurrenceService>();
        services.AddScoped<IScoreService, ScoreService>();

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
