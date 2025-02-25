using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalLibrary.Server.Model
{
    [Table("DocumentSubject_HD")]
    public class DocumentSubject
    {
        [Key, Column(Order = 0)]
        public int documentid { get; set; }
        [Key, Column(Order = 1)]
        public int subjectid { get; set; }

        [ForeignKey("documentid")]       
        public Documents document {  get; set; }

        [ForeignKey("subjectid")]
        public Subjects subject { get; set; }
    }
}
