using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Client.Infrastructure.Managers.Identity.Roles.Contracts
{
    public interface IRoleManager
    {
        Task<CommandResult<List<RoleResponse>>> GetRolesAsync();

        Task<CommandResult<string>> SaveAsync(RoleRequest role);

        Task<CommandResult<string>> DeleteAsync(string id);

        Task<CommandResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<CommandResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}
