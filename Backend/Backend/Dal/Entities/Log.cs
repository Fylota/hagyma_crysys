using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dal.Entities
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] public DateTime CreationDate { get; set; }
        [Required] public string Level { get; set; } = null!;
        [Required] public string Message { get; set; } = null!;
    }
}
