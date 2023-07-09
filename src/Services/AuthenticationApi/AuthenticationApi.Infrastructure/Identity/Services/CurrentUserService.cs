using System.Security.Claims;
using AuthenticationApi.Application.Contracts.Services;
using Microsoft.AspNetCore.Http;

namespace AuthenticationApi.Infrastructure.Identity.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            Claims = httpContextAccessor.HttpContext?.User;
        }

        public string UserId { get; }
        public string? Email { get; }
        public ClaimsPrincipal Claims { get; set; }
    }
}
