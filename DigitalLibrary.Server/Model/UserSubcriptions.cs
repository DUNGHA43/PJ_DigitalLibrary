using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("UserSubscriptions_HD")]
    public class UserSubcriptions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int? userid { get; set; }
        public int? planid { get; set; }

        public DateTime redate { get; set; }
        public DateTime exdate { get; set; }
        public bool status { get; set; }

        [ForeignKey("userid")]
        public Users user {  get; set; }

        [ForeignKey("planid")]
        public SubscriptionPlans plan { get; set; }
    }
}
