using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class PlanRevenueDTO
    {
        public string PlanName { get; set; } = string.Empty;
        public int TotalTransactions { get; set; }
        public decimal? TotalRevenue { get; set; }

    }
}
