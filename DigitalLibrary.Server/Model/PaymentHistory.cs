using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("PaymentHistory_HD")]
    public class PaymentHistory
    {
        public int id { get; set; }
        public DateTime createDate { get; set; }
        public int userId { get; set; }
        public int planId { get; set; }
    }
}
