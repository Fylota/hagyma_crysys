using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Backend.Dal.Entities;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class Log
{
    [Required] public DateTime CreationDate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Level { get; set; } = null!;
    [Required] public string Message { get; set; } = null!;
}