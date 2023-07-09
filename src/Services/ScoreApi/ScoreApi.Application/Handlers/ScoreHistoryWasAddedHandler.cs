using MediatR;
using ScoreApi.Application.Occurrences.Contracts;
using ScoreApi.Application.Scores.Contracts;
using ScoreApi.Domain.Aggregates.ScoreAggregate.DomainEvents;
using Shared.Logs.Services;

namespace ScoreApi.Application.Handlers;

public class ScoreHistoryWasAddedHandler :INotificationHandler<ScoreHistoryWasAdded>
{
    private readonly IScoreService _scoreService;
    private readonly IOccurrenceService _occurrenceService;
    private readonly ILogServices _logServices;

    public ScoreHistoryWasAddedHandler(IScoreService scoreService, IOccurrenceService occurrenceService, ILogServices logServices)
    {
        _scoreService = scoreService;
        _occurrenceService = occurrenceService;
        _logServices = logServices;
    }

    public async Task Handle(ScoreHistoryWasAdded notification, CancellationToken cancellationToken)
    {
        _logServices.WriteMessage($"An new ScoreHistory was added!");
        var ocurrenceThatWasAddedResult = await _occurrenceService.GetOccurrenceById(notification.ScoreHistory.OccurrenceId);
        var scoreResult = await _scoreService.GetScoreById(notification.ScoreHistory.ScoreId);
        
        if (ocurrenceThatWasAddedResult.Succeeded && scoreResult.Succeeded)
        {
            _logServices.WriteMessage("Score and occurrence fetched successfully!");
            var ocurrence = ocurrenceThatWasAddedResult.Data;
            var score = scoreResult.Data;
            var updateScoreResult = await _scoreService.UpdateUserScoreValue(score.UserId, ocurrence.Value);
            
            if(updateScoreResult.Succeeded)
                _logServices.WriteMessage($"Score value updated successfully!");
            else
                _logServices.WriteMessage($"Failed to update score value");
        }
    }
}