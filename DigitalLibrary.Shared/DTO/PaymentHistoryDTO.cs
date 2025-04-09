using DigitalLibrary.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class PaymentHistoryDTO
    {
        public int id { get; set; }
        public DateTime createDate { get; set; }
        public int userId { get; set; }
        public int planId { get; set; }

        public SubscriptionPlansDTO SubscriptionPlans { get; set; }
    }
}
