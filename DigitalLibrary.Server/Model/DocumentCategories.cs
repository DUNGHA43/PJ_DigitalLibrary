using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("DocumentCategorise_HD")]
    public class DocumentCategories
    {
        [Key, Column(Order = 0)]
        public int documentid { get; set; }
        [Key, Column(Order = 1)]
        public int categoryid { get; set; }

        [ForeignKey("documentid")]
        public Documents document { get; set; }

        [ForeignKey("categoryid")]
        public Categories category { get; set; }
    }
}
