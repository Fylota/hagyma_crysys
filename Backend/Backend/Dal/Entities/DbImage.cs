using System.ComponentModel.DataAnnotations;

namespace Backend.Dal.Entities;

public class DbImage
{
    public bool IsDeleted { get; set; }
    [Required] public byte[] CaffFile { get; set; } = null!;
    [Required] public DateTime UploadTime { get; set; }
    public ICollection<DbComment> Comments { get; set; } = null!;
    [Required] public string CaffFileName { get; set; } = null!;
    [Required] public string Description { get; set; } = null!;
    public string Id { get; set; } = null!;
    [Required] public string OwnerId { get; set; } = null!;
    [Required] public string Preview { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
}