using System.Security.Claims;
using Aplication.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastruture.Services;

public class UserAccessor: IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims
            .First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        return userId;
    }
}