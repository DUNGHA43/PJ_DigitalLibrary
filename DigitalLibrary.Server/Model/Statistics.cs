using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public Documents Document { get; set; }
    }
}
