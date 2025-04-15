using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Shared.DTO
{
    public class ChangePassDTO
    {
        public int userid { get; set; }
        public string email { get; set; }
        public string newPass { get; set; }
    }
}
