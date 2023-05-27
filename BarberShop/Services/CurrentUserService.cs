using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BarberShop.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User ?? throw new InvalidOperationException("No authenticated user!");

        UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    public string UserId { get; }
}