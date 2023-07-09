using ScoreApi.Domain.Aggregates.ScoreAggregate.DomainEvents;
using ScoreApi.Domain.SeedWork;

namespace ScoreApi.Domain.Aggregates.ScoreAggregate;

public class Score : Entity
{
    public string UserId { get; private set; }
    public int Value { get; private set; }
    public List<ScoreHistory> ScoreHistories { get; set; } = new();

    public Score(string userId, int value)
    {
        UserId = userId;
        Value = value;
    }

    public void AddScoreHistory(int occurrenceId)
    {
        var scoreHistory = new ScoreHistory(Id, occurrenceId);
        ScoreHistories.Add(scoreHistory);

        AddDomainEvent(new ScoreHistoryWasAdded(scoreHistory));
    }

    public void UpdateValue(int valueToBeAdded)
    {
        Value += valueToBeAdded;
    }

    public bool CanApplyToLoanWithCurrentScore()
    {
        if (Value >= 500)
            return true;

        return false;
    }
}