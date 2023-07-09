using Shared.Entities;
using Shared.Requests;

namespace IdentityApi.Application.Services.Identity.Contracts;

public interface IAccountService
{
    Task<CommandResult<string>> Login(LoginRequest loginDto);
    Task<ICommandResult> Register(RegisterRequest registerDto);

}
