using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("Users_HD")]
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int? roleid { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? fullname { get; set; }
        public bool? gender { get; set; }
        public DateTime? birthday { get; set; }
        public string? phonenumber { get; set; }
        public string? identification { get; set; }
        public string? address { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
        public string? refreshtoken { get; set; }
        public DateTime? refreshtokenexpirytime { get; set; }


        [ForeignKey("roleid")]
        public Roles role { get; set; }

        public UserPermissions permission { get; set; }

        public ICollection<Reviews> reviews { get; set; } = new List<Reviews>();

        public UserSubcriptions usersubcription { get; set; }
    }
}
