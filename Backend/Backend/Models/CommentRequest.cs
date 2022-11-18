using System.ComponentModel.DataAnnotations;
using Backend.Dal.Entities;

namespace Backend.Models;

public class CommentRequest
{
    [Required] public string Content { get; set; } = null!;

}