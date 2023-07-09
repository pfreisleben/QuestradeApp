using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace AuthenticationApi.Application.Services.Identity.Contracts
{
    public interface IUserService 
    {
        Task<CommandResult<List<UserResponse>>> GetAllAsync();

        Task<CommandResult<UserResponse>> GetAsync(string userId);

        Task<CommandResult<UserRolesResponse>> GetRolesAsync(string id);

        Task<ICommandResult> UpdateRolesAsync(UpdateUserRolesRequest request);

        Task<ICommandResult> UpdateUserStatusAsync(UpdateUserStatusRequest request);
        Task<ICommandResult> UpdateUserAsync(UserRequest request);

    }
}