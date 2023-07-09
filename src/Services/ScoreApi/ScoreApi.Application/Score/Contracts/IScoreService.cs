using ScoreApi.Domain.Aggregates.ScoreAggregate;
using Shared.Entities;
using Shared.Requests.Score;

namespace ScoreApi.Application.Scores.Contracts;

public interface IScoreService
{
    Task<CommandResult> InitializeUserScore(InitializeUserScoreRequest request);
    Task<CommandResult<Score>> GetScoreByUserId(string userId);
    Task<CommandResult> UpdateScoreHistory(UpdateScoreRequest request);
    Task<CommandResult> AddScore(AddScoreRequest request);
    Task<CommandResult> UpdateUserScoreValue(string userId, int valueToBeAdded);
    Task<CommandResult<Score>> GetScoreById(int scoreId);
    Task<CommandResult<bool>> CheckIfCanApplyToLoan(CheckIfCanApplyToLoanRequest request);
}