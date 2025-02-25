using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class DocumentsDTO
    {
        public int id {  get; set; }
        public string? title { get; set; }
        public string? publisher { get; set; }
        public string? imagepath { get; set; }
        public string? imageurl { get; set; }
        public string? filepath { get; set; }
        public string? fileurl { get; set; }
        public int? uploadedby { get; set; }
        public string? description { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
        public string? accesslevel { get; set; }
    }
}
