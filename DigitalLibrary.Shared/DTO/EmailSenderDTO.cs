using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibarary.Shared.DTO
{
    public class EmailSenderDTO
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Code { get; set; }
    }
}
