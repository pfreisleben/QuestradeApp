using ScoreApi.Domain.Entities;
using ScoreApi.Domain.SeedWork;

namespace ScoreApi.Domain.Aggregates.ScoreAggregate;

public class ScoreHistory : Entity
{
    public int ScoreId { get; set; }
    public int OccurrenceId { get; set; }
    public Occurrence Occurrence { get; set; } = null!;

    public ScoreHistory(int scoreId, int occurrenceId)
    {
        ScoreId = scoreId;
        OccurrenceId = occurrenceId;
    }
}