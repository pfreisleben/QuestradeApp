using Microsoft.EntityFrameworkCore;
using ScoreApi.Application.Occurrences.Contracts;
using ScoreApi.Domain.Entities;
using ScoreApi.Infrastructure.Persistence.AppDb;
using Shared.Entities;

namespace ScoreApi.Infrastructure.Occurrences;

public class OccurrenceService : IOccurrenceService
{
    private readonly ApplicationDbContext _context;

    public OccurrenceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CommandResult<Occurrence>> GetOccurrenceById(int occurrenceId)
    {
        var occurence = await _context.Occurrences
            .AsNoTracking()
            .Where(x => x.Id == occurrenceId)
            .FirstOrDefaultAsync();

        if (occurence is null)
            return await CommandResult<Occurrence>.FailAsync("Occurrence does not exist!");

        return await CommandResult<Occurrence>.SuccessAsync(occurence);
    }
}