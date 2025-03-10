using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public  class SubjectDTO
    {
        public int id {  get; set; }
        public string? subjectname { get; set; }
        public DateTime? createdate { get; set; }
        public bool status { get; set; }
    }
}
