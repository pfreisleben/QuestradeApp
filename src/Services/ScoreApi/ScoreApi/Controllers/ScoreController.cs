using Microsoft.AspNetCore.Mvc;
using ScoreApi.Application.Scores.Contracts;
using Shared.Entities;
using Shared.Requests.Score;

namespace ScoreApi.Controllers;

[Route("api/score/")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public AccountController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpPost("AddScore")]
    public async Task<ActionResult<CommandResult>> Login([FromBody] AddScoreRequest request)
    {
        var retorno = await _scoreService.AddScore(request);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
    
    [HttpPost("InitializeUserScore")]
    public async Task<ActionResult<CommandResult>> Login([FromBody] InitializeUserScoreRequest request)
    {
        var retorno = await _scoreService.InitializeUserScore(request);

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
}