using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class User
{
    [Required] public string Email { get; set; } = null!;
    [Required] public string Id { get; set; } = null!;
    [Required] public string Name { get; set; } = null!;
}