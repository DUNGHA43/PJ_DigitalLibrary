using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class StatisticDTO
    {
        public int id {  get; set; }
        public int? documentid {  get; set; }
        public long? views { get; set; }
        public long? dowloaded { get; set; }
    }
}
