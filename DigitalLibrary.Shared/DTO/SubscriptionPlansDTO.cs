using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class SubscriptionPlansDTO
    {
        public int id {  get; set; }
        public string? nameplan { get; set; }
        public decimal? price { get; set; }
        public int? durationsindays { get; set; }
        public string? description { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}
