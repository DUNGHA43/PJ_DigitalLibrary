using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("DocumentAuthors_HD")]
    public class DocumentAuthors
    {
        [Key, Column(Order = 0)]
        public int documentid { get; set; }
        [Key, Column(Order = 1)]
        public int authorid { get; set; }

    }
}
