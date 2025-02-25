using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Roles_HD")]
    public class Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? rolenameen { get; set; }
        public string? rolenamevn { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }

        public ICollection<Users> users { get; set; } = new List<Users>();
    }
}
