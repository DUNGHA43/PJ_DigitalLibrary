using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class TrafficLogDTO
    {
        public int id { get; set; }
        public string? ipaddress { get; set; }
        public string? useragent { get; set; }
        public string? url { get; set; }
        public DateTime? createdate { get; set; }
    }
}
