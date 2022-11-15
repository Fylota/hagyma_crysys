namespace Backend.Models.Auth;

public class AuthUser
{
    public AuthRoles Role { get; set; }
    public string UserName { get; set; } = null!;
}