using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Comment
{
    public DateTime? CreationTime { get; set; }

    [Required] public string Content { get; set; } = null!;
    [Required] public string Id { get; set; } = null!;
}