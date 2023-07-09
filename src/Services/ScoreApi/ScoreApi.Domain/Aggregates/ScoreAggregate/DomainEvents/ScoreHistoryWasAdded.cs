using MediatR;

namespace ScoreApi.Domain.Aggregates.ScoreAggregate.DomainEvents;

public record ScoreHistoryWasAdded(ScoreHistory ScoreHistory) : INotification;