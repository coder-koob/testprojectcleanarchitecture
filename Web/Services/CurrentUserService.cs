using System.Security.Claims;
using Domain.Interfaces;

namespace Web.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? ClientId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("client_id");
}