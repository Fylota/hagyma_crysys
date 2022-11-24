using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CommentRequest
{
    [Required] public string Content { get; set; } = null!;
}