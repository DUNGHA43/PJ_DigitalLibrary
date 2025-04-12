using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class ViewAndDowloadStatisticResponse
    {
        public List<ChartDataStatisticDocumentDTO> ChartData { get; set; }

        public List<StatisticDTO> FullData { get; set; } = new List<StatisticDTO>();
        public List<StatisticDTO> TopViewsDocuments { get; set; } = new List<StatisticDTO>();
        public List<StatisticDTO> TopDowloadedDocuments { get; set; } = new List<StatisticDTO>();

        public long? TotalViews { get; set; }
        public long? TotalDownloads { get; set; }
    }
}
