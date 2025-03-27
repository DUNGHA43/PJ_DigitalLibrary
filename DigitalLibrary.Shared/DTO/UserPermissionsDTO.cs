using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class UserPermissionsDTO
    {
        public int userid {  get; set; }
        public bool canread { get; set; }
        public bool candowload { get; set; }
    }
}
