using System.Net.Http.Json;
using Client.Infrastructure.Extensions;
using Client.Infrastructure.Routes;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Client.Infrastructure.Managers.Identity.Users
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;

        public UserManager(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientNames.AuthenticationApi);
        }

        public async Task<ICommandResult> UpdateUserStatusAsync(UpdateUserStatusRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateUserStatus, request);
            return await response.ToResult<string>();
        }

        public async Task<CommandResult<List<UserResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(UserEndpoints.GetAll);
            return await response.ToResult<List<UserResponse>>();
        }

        public async Task<CommandResult<UserResponse>> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync(UserEndpoints.Get(userId));
            return await response.ToResult<UserResponse>();
        }

        public async Task<CommandResult<UserRolesResponse>> GetRolesAsync(string userId)
        {
            var response = await _httpClient.GetAsync(UserEndpoints.GetUserRoles(userId));
            return await response.ToResult<UserRolesResponse>();
        }

        public async Task<ICommandResult> UpdateRolesAsync(UpdateUserRolesRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(UserEndpoints.GetUserRoles(request.UserId), request);
            return await response.ToResult<UserRolesResponse>();
        }

        public async Task<ICommandResult> UpdateUserAsync(UserRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(UserEndpoints.UpdateUser, request);
            return await response.ToResult();
        }


    }
}