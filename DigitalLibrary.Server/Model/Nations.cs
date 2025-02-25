using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Nations_HD")]
    public class Nations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? nationame { get; set; }
        public string? nationiso { get; set; }

        public Authors author {  get; set; }
    }
}
