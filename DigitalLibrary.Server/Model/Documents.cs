using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Documents_HD")]
    public class Documents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? title { get; set; }
        public string? publisher { get; set; }
        public string? imagepath { get; set; }
        public string? imageurl { get; set; }
        public string? filepath { get; set; }
        public string? fileurl { get; set; }
        public int? uploadedby { get; set; }
        public string? description { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
        public string? accesslevel { get; set; }

        public ICollection<Reviews> reviews { get; set; } = new List<Reviews>();
        public Statistics statistic { get; set; }
    }
}
