using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CaffItem
{
    [Required] public string Id { get; set; } = null!;

    [Required] public string Preview { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
}