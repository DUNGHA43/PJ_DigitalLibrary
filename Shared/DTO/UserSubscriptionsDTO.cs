using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class UserSubscriptionsDTO
    {
        public int id { get; set; }
        public int? userid { get; set; }
        public int? planid { get; set; }
        public DateTime redate { get; set; }
        public DateTime exdate { get; set; }
        public string status { get; set; }
    }
}
