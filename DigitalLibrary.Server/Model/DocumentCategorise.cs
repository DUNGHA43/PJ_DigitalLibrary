using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("DocumentsCategorise_HD")]
    public class DocumentCategorise
    {
        [Key, Column(Order = 0)]
        public int documentid { get; set; }
        [Key, Column(Order = 1)]
        public int categoryid { get; set; }

        [ForeignKey("documentid")]
        public Documents document { get; set; }

        [ForeignKey("categoryid")]
        public Categorise category { get; set; }
    }
}
