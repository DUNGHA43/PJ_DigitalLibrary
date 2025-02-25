using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Statistic_HD")]
    public class Statistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int? documentid { get; set; }
        public long? views { get; set; }
        public long? dowloaded { get; set; }

        [ForeignKey("documentid")]
        public Documents Document { get; set; }
    }
}
