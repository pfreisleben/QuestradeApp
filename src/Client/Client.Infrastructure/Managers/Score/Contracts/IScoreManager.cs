using Shared.Entities;
using Shared.Requests.Score;

namespace Client.Infrastructure.Managers.Score.Contracts;

public interface IScoreManager
{
    Task<CommandResult<ScoreResponse>> GetScoreByUserId(string userId);
    Task<CommandResult<bool>> CheckIfUserCanApplyToLoan(string userId);
}