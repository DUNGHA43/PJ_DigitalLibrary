using DigitalLibrary.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class DocumentGroupDTO
    {
        public string? GroupKey { get; set; }
        public List<DocumentsDTO> Documents { get; set; } = new List<DocumentsDTO>();
        public int TotalCount { get; set; }
    }
}
