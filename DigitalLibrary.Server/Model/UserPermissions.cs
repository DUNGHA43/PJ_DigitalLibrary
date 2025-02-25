using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("UserPermissions_HD")]
    public class UserPermissions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }
        [Required]
        public bool canread { get; set; }
        public bool candowload { get; set; }

        [ForeignKey("userid")]
        public Users user { get; set; }

    }
}
