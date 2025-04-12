using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class ChartDataStatisticDocumentDTO
    {
        public string DocumentName { get; set; }
        public long? Views { get; set; }
        public long? Downloads { get; set; }
    }
}
