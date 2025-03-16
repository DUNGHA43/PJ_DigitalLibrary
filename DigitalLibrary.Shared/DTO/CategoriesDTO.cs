using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class CategoriesDTO
    {
        public int id {  get; set; }
        public string? catename { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}
