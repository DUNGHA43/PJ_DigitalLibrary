using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class ChartDataDTO
    {
        public string[] Labels { get; set; } = Array.Empty<string>();
        public int[] Data { get; set; } = Array.Empty<int>();

    }
}
