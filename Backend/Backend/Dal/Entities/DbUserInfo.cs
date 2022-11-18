using Backend.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace Backend.Dal.Entities;

public class DbUserInfo : IdentityUser
{
    public AuthRoles Role { get; set; }
    public ICollection<DbImage> PurchasedImages { get; set; } = null!;
}