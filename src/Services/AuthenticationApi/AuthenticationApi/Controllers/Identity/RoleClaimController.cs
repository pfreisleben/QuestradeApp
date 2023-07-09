using AuthenticationApi.Application.Services.Identity.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests;

namespace AuthenticationApi.Controllers.Identity;

[Route("api/identity/roleClaim")]
[ApiController]
public class RoleClaimController : ControllerBase
{
    private readonly IRoleClaimService _roleClaimService;

    public RoleClaimController(
        IRoleClaimService roleClaimService)
    {
        _roleClaimService = roleClaimService;
    }


    [Authorize(Policy = Permissions.RoleClaims.View)]
    [HttpGet]
    public async Task<ActionResult<ICommandResult>> GetAll()
    {
        var retorno = await _roleClaimService.GetAllAsync();
        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }

    [Authorize(Policy = Permissions.RoleClaims.Create)]
    [HttpPost]
    public async Task<ActionResult<CommandResult>> Post(RoleClaimRequest request)
    {
        var retorno = await _roleClaimService.SaveAsync(request);
        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }


    [Authorize(Policy = Permissions.RoleClaims.View)]
    [HttpGet("{roleId}")]
    public async Task<ActionResult<CommandResult>> GetAllByRoleId([FromRoute] string roleId)
    {
        var retorno = await _roleClaimService.GetAllByRoleIdAsync(roleId);
        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }


    [Authorize(Policy = Permissions.RoleClaims.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CommandResult>> Delete(int id)
    {
        var retorno = await _roleClaimService.DeleteAsync(id);
        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
}