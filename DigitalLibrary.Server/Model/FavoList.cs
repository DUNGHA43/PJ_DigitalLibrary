using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("FavoList_HD")]
    public class FavoList
    {
        [Key, Column(Order = 0)]
        public int userid { get; set; }
        [Key, Column(Order = 1)]
        public int documentid { get; set; }
    }
}
