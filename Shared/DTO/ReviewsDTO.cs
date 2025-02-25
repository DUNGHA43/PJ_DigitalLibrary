using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class ReviewsDTO
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int documentid { get; set; }
        public int? rating { get; set; }
        public string? comment { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}
