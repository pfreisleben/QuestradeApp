using ScoreApi.Domain.Entities;
using Shared.Entities;

namespace ScoreApi.Application.Occurrences.Contracts;

public interface IOccurrenceService
{
    Task<CommandResult<Occurrence>> GetOccurrenceById(int occurrenceId);
    Task<CommandResult<List<Occurrence>>> GetOccurrences();
}