using System.ComponentModel.DataAnnotations;

namespace Backend.Dal.Entities
{
    public class DbImage
    {
        public string Id { get; set; } = null!;
        [Required] public byte[] CaffFile { get; set; } = null!;
        [Required] public string CaffFileName { get; set; } = null!;
        [Required] public string Preview { get; set; } = null!;
        [Required] public string OwnerId { get; set; } = null!;
        [Required] public DateTime UploadTime { get; set; }
        [Required] public string Title { get; set; } = null!;
        [Required] public string Description { get; set; } = null!;
        public ICollection<DbComment> Comments { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
