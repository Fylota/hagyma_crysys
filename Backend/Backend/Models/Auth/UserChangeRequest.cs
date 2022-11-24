using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Auth;

public class UserChangeRequest
{
    [Required] public string CurrentPassword { get; set; } = null!;
    [Required] public string NewPassword { get; set; } = null!;
}