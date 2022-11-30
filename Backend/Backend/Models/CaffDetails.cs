namespace Backend.Models;

public class CaffDetails : CaffItem
{
    public List<Comment> Comments { get; set; } = new();

    public string Description { get; set; } = null!;

    public DateTime UploadTime { get; set; }
}