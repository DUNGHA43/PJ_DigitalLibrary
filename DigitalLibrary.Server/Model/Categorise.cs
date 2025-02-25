using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Categorise_HD")]
    public class Categorise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? catename { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }

    }
}
