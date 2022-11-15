using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dal.Entities;

public class DbComment
{
    [Required] public DateTime CreatedDate { get; set; }
    public string Id { get; set; } = null!;
    [ForeignKey(nameof(DbImage))] public string DbImageId { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
    [Required] public string UserId { get; set; } = null!;
}