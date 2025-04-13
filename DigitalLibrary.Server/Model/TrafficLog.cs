using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.Server.Model
{
    [Table("TrafficLog_HD")]
    public class TrafficLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? ipaddress { get; set; }
        public string? useragent { get; set; }
        public string? url { get; set; }
        public DateTime? createdate { get; set; }
    }
}
