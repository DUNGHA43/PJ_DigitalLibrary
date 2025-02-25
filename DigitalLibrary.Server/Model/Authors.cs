
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Authors_HD")]
    public class Authors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? fullname { get; set; }
        public DateTime? birthday { get; set; }
        public int? nationalityid { get; set; }
        public string? description { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }

        [ForeignKey("nationalityid")]
        public Nations nation { get; set; }
    }
}
