using System.Security.Claims;
using Shared.Entities;
using Shared.Requests;

namespace Client.Infrastructure.Managers.Identity.Authentication.Contracts;

public interface IAuthenticationManager
{
    Task<ICommandResult> Login(LoginRequest loginModel);
    Task Logout();
    Task<CommandResult> Register(RegisterRequest registerModel);
    Task<ClaimsPrincipal> CurrentUser();
}