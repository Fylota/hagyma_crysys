using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Auth;

public class RegisterRequest
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}