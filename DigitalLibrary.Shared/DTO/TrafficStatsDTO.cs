using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class TrafficStatsDTO
    {
        public int TotalVisits { get; set; }
        public int TodayVisits { get; set; }
        public int ThisMonthVisits { get; set; }

        public List<DailyTraffic> DailyStats { get; set; } = new();
        public List<PageViewStats> TopPages { get; set; } = new();
    }
}
