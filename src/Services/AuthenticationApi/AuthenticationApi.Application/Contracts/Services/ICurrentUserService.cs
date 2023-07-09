using System.Security.Claims;

namespace AuthenticationApi.Application.Contracts.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string? Email { get; }
        ClaimsPrincipal Claims { get; }
    }
}