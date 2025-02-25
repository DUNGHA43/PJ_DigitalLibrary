using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class RolesDTO
    {
        public int id { get; set; }
        public string? rolenameen { get; set; }
        public string? rolenamevn { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}
