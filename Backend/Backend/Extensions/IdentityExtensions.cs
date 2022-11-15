using System.Security.Claims;
using System.Security.Principal;
using Backend.Helpers;

namespace Backend.Extensions;

public static class IdentityExtensions
{
    public static string? GetUserName(this IPrincipal user)
    {
        return ((ClaimsPrincipal)user).FindFirst(ClaimTypes.Name)?.Value;
    }

    public static string? GetUserId(this IPrincipal user)
    {
        return ((ClaimsPrincipal)user).FindFirst(Constants.UserId)?.Value;
    }

    public static string? GetEmail(this IPrincipal user)
    {
        return ((ClaimsPrincipal)user).FindFirst(ClaimTypes.Email)?.Value;
    }

    public static string? GetRole(this IPrincipal user)
    {
        return ((ClaimsPrincipal)user).FindFirst(ClaimTypes.Role)?.Value;
    }
}