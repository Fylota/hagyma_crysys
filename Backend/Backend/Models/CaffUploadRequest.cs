using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class CaffUploadRequest
{
    [Required] public IFormFile CaffFile { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
}