using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Reviews_HD")]
    public class Reviews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int userid { get; set; }
        public int documentid { get; set; }

        public int? rating { get; set; }
        public string? comment { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }

        [ForeignKey("userid")]
        public Users user { get; set; }

        [ForeignKey("userid")]
        public Documents document { get; set; }
    }
}
