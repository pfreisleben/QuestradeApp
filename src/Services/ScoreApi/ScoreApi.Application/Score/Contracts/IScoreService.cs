using Shared.Entities;
using Shared.Requests.Score;

namespace ScoreApi.Application.Score.Contracts;

public interface IScoreService
{
    Task<CommandResult> InitializeUserScore(InitializeUserScoreRequest request);
    Task<CommandResult<Domain.Aggregates.ScoreAggregate.Score>> GetScoreByUserId(string userId);
    Task<CommandResult> UpdateScoreHistory(UpdateScoreRequest request);
    Task<CommandResult> AddScore(AddScoreRequest request);
    Task<CommandResult> UpdateUserScoreValue(string userId, int valueToBeAdded);
    Task<CommandResult<Domain.Aggregates.ScoreAggregate.Score>> GetScoreById(int scoreId);
    Task<CommandResult<bool>> CheckIfCanApplyToLoan(string userId);
}