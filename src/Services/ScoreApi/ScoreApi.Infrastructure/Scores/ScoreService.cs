using Microsoft.EntityFrameworkCore;
using ScoreApi.Application.Scores.Contracts;
using ScoreApi.Domain.Aggregates.ScoreAggregate;
using ScoreApi.Domain.Aggregates.ScoreAggregate.DomainExceptions;
using ScoreApi.Infrastructure.Persistence.AppDb;
using Shared.Entities;
using Shared.Requests.Score;

namespace ScoreApi.Infrastructure.Scores;

public class ScoreService : IScoreService
{
    private readonly ApplicationDbContext _context;

    public ScoreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CommandResult> InitializeUserScore(InitializeUserScoreRequest request)
    {
        var userScore = new Score(request.UserId, 0);
        userScore.AddScoreHistory(7);

        await _context.AddAsync(userScore);
        await _context.SaveChangesAsync();

        return await CommandResult.SuccessAsync("Score adicionado com sucesso!");
    }

    public async Task<CommandResult> UpdateScoreHistory(UpdateScoreRequest request)
    {
        var userScore = await _context.Scores.Where(x => x.UserId == request.UserId).FirstOrDefaultAsync();

        if (userScore is null)
            return await CommandResult.FailAsync(ScoreDomainExceptions.UserScoreDoesNotExist);

        userScore.AddScoreHistory(request.OccurrenceId);

        await _context.SaveChangesAsync();
        return await CommandResult.SuccessAsync("Score atualizado com sucesso!");
    }

    public async Task<CommandResult> AddScore(AddScoreRequest request)
    {
        var userScore = new Score(request.UserId, 0);
        userScore.AddScoreHistory(request.OccurrenceId);

        await _context.AddAsync(userScore);
        await _context.SaveChangesAsync();

        return await CommandResult.SuccessAsync("Score adicionado com sucesso!");
    }

    public async Task<CommandResult> UpdateUserScoreValue(string userId, int valueToBeAdded)
    {
        var userScore = await _context.Scores.Where(x => x.UserId == userId).FirstOrDefaultAsync();

        if (userScore is null)
            return await CommandResult.FailAsync(ScoreDomainExceptions.UserScoreDoesNotExist);

        userScore.UpdateValue(valueToBeAdded);

        await _context.SaveChangesAsync();
        return await CommandResult.SuccessAsync("Score atualizado com sucesso");
    }

    public async Task<CommandResult<Score>> GetScoreById(int scoreId)
    {
        var score = await _context.Scores.Where(x => x.Id == scoreId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (score is null)
            return await CommandResult<Score>.FailAsync("Score does not exist");
        
        return await CommandResult<Score>.SuccessAsync(score);
    }

    public async Task<CommandResult<bool>> CheckIfCanApplyToLoan(CheckIfCanApplyToLoanRequest request)
    {
        var userScore = await _context.Scores
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .FirstOrDefaultAsync();

        if (userScore is null)
            return await CommandResult<bool>.FailAsync(ScoreDomainExceptions.UserScoreDoesNotExist);

        return await CommandResult<bool>.SuccessAsync(userScore.CanApplyToLoanWithCurrentScore());
    }
}