using AuthenticationApi.Application.Contracts.Services;
using AuthenticationApi.Application.Services.Identity.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
using Shared.Logs.Services;
using Shared.Requests;

namespace AuthenticationApi.Controllers.Identity;

[Route("api/identity/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAccountService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogServices _logServices;

    public AccountController(IConfiguration configuration,
        ILogServices logServices, IAccountService identityService,
        ICurrentUserService currentUserService)
    {
        _configuration = configuration;
        _logServices = logServices;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<CommandResult>> Login([FromBody] LoginRequest login)
    {
        var retorno = await _identityService.Login(login);
        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<CommandResult>> Register([FromBody] RegisterRequest model)
    {
        var retorno = await _identityService.Register(model);
        if (retorno.Succeeded)
            return Ok(retorno);
        return BadRequest(retorno);
    }
}