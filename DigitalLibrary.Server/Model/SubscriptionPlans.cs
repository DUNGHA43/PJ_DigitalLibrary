using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("SubscriptionPlans_HD")]
    public class SubscriptionPlans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string? nameplan { get; set; }
        public decimal? price { get; set; }
        public int? durationsindays { get; set; }
        public string? description { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }

        public UserSubcriptions userSubcription { get; set; }
    }
}
