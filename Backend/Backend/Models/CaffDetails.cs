namespace Backend.Models;

public class CaffDetails : CaffItem
{
    public List<Comment> Comments { get; set; } = new();

    public string Description { get; set; } = null!;

    public DateTime UploadTime { get; set; }

    public bool CanDownload { get; set; }

    public string OwnerId { get; set; } = null!;
}