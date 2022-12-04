using System.Security.Claims;
using System.Security.Principal;
using Backend.Helpers;

namespace Backend.Extensions;

public static class IdentityExtensions
{
    public static string? GetUserId(this IPrincipal user)
    {
        return ((ClaimsPrincipal) user).FindFirst(Constants.UserId)?.Value;
    }
}