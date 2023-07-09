using Microsoft.AspNetCore.Mvc;
using ScoreApi.Application.Occurrences.Contracts;
using Shared.Entities;

namespace ScoreApi.Controllers;

[Route("api/occurrence/")]
[ApiController]
public class AccorrenceController : ControllerBase
{
    private readonly IOccurrenceService _occurrenceService;

    public AccorrenceController(IOccurrenceService occurrenceService)
    {
        _occurrenceService = occurrenceService;
    }


    [HttpGet("GetOccurrences")]
    public async Task<ActionResult<CommandResult>> GetOccurrences()
    {
        var retorno = await _occurrenceService.GetOccurrences();

        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
    
}