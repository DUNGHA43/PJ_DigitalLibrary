using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class MonthlyPlanRevenueDTO
    {
        public int Month { get; set; }
        public string PlanName { get; set; }
        public int TotalTransactions { get; set; }
        public decimal? TotalRevenue { get; set; }
    }
}
