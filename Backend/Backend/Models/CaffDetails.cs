namespace Backend.Models;

public class CaffDetails : CaffItem
{
    public bool CanDownload { get; set; }

    public DateTime UploadTime { get; set; }
    public List<Comment> Comments { get; set; } = new();

    public string Description { get; set; } = null!;

    public string OwnerId { get; set; } = null!;
}