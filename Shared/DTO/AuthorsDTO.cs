using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class AuthorsDTO
    {
        public int id { get; set; }
        public string? fullname { get; set; }
        public DateTime? birthday { get; set; }
        public int? nationalityid { get; set; }
        public string? description { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}
