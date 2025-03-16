using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTO
{
    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages
        {
            get
            {
                return PageSize > 0 ? (int)Math.Ceiling((double)TotalRecords / PageSize) : 1;
            }
        }
    }
}
