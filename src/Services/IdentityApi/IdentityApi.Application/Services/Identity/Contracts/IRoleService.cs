using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace IdentityApi.Application.Services.Identity.Contracts
{
    public interface IRoleService
    {
        Task<CommandResult<List<RoleResponse>>> GetAllAsync();
        Task<CommandResult<string>> SaveAsync(RoleRequest request);

        Task<CommandResult<string>> DeleteAsync(string id);

        Task<CommandResult<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<CommandResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}
