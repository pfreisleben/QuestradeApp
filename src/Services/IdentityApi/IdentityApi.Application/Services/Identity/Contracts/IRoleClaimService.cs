using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace IdentityApi.Application.Services.Identity.Contracts;

public interface IRoleClaimService
{
    Task<CommandResult<List<RoleClaimResponse>>> GetAllAsync();

    Task<CommandResult<RoleClaimResponse>> GetByIdAsync(int id);

    Task<CommandResult<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

    Task<CommandResult<string>> SaveAsync(RoleClaimRequest request);

    Task<CommandResult<string>> DeleteAsync(int id);
}
