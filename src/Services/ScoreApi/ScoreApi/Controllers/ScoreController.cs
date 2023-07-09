using Microsoft.AspNetCore.Mvc;
using ScoreApi.Application.Score.Contracts;
using Shared.Entities;
using Shared.Requests.Score;

namespace ScoreApi.Controllers;

[Route("api/score/")]
[ApiController]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoreController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpPost("AddScore")]
    public async Task<ActionResult<CommandResult>> AddScore([FromBody] AddScoreRequest request)
    {
        var retorno = await _scoreService.AddScore(request);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }

    [HttpPost("InitializeUserScore")]
    public async Task<ActionResult<CommandResult>> InitializeUserScore([FromBody] InitializeUserScoreRequest request)
    {
        var retorno = await _scoreService.InitializeUserScore(request);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }

    [HttpGet("GetUserScore/{userId}")]
    public async Task<ActionResult<CommandResult>> GetUserScore(string userId)
    {
        var retorno = await _scoreService.GetScoreByUserId(userId);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
    
    [HttpGet("CheckIfCanApplyToLoan/{userId}")]
    public async Task<ActionResult<CommandResult>> CheckIfCanApplyToLoan(string userId)
    {
        var retorno = await _scoreService.CheckIfCanApplyToLoan(userId);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
}