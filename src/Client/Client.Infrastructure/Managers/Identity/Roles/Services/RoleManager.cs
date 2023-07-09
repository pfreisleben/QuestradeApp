using System.Net.Http.Json;
using Client.Infrastructure.Extensions;
using Client.Infrastructure.Managers.Identity.Roles.Contracts;
using Client.Infrastructure.Routes;
using Shared.Constants;
using Shared.Entities;
using Shared.Requests;
using Shared.Responses;

namespace Client.Infrastructure.Managers.Identity.Roles.Services;

public class RoleManager : IRoleManager
{
    private readonly HttpClient _httpClient;

    public RoleManager(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.AuthenticationApi);
    }

    public async Task<CommandResult<string>> DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{RolesEndpoints.Delete}/{id}");
        return await response.ToResult<string>();
    }

    public async Task<CommandResult<PermissionResponse>> GetPermissionsAsync(string roleId)
    {
        var response = await _httpClient.GetAsync(RolesEndpoints.GetPermissions + roleId);
        return await response.ToResult<PermissionResponse>();
    }

    public async Task<CommandResult<List<RoleResponse>>> GetRolesAsync()
    {
        var response = await _httpClient.GetAsync(RolesEndpoints.GetAll);
        return await response.ToResult<List<RoleResponse>>();
    }

    public async Task<CommandResult<string>> SaveAsync(RoleRequest role)
    {
        var response = await _httpClient.PostAsJsonAsync(RolesEndpoints.Save, role);
        return await response.ToResult<string>();
    }

    public async Task<CommandResult<string>> UpdatePermissionsAsync(PermissionRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync(RolesEndpoints.UpdatePermissions, request);
        return await response.ToResult<string>();
    }
}
