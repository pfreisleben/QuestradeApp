using Shared.Entities;
using Shared.Requests.Score;

namespace FinanceApi.Application.Scores;

public interface IScoreService
{
    Task<CommandResult> AddScore(AddScoreRequest request);
}