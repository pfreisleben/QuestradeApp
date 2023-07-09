using System.Security.Claims;

namespace ScoreApi.Application.Contracts.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string? Email { get; }
        ClaimsPrincipal Claims { get; }
    }
}