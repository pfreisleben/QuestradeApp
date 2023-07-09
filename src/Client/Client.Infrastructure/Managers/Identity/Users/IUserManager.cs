using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Client.Infrastructure.Managers.Identity.Users
{
    public interface IUserManager
    {
        Task<CommandResult<List<UserResponse>>> GetAllAsync();
        Task<CommandResult<UserResponse>> GetAsync(string userId);
        Task<CommandResult<UserRolesResponse>> GetRolesAsync(string userId);
        Task<ICommandResult> UpdateRolesAsync(UpdateUserRolesRequest request);
        Task<ICommandResult> UpdateUserStatusAsync(UpdateUserStatusRequest request);
        Task<ICommandResult> UpdateUserAsync(UserRequest request);

    }
}