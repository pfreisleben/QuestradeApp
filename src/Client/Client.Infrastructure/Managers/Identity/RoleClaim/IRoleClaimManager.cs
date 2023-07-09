using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Client.Infrastructure.Managers.Identity.RoleClaim;

public interface IRoleClaimManager
{
    Task<CommandResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

    Task<CommandResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

    Task<CommandResult<string>> SaveAsync(RoleClaimRequest role);

    Task<CommandResult<string>> DeleteAsync(string id);
}
