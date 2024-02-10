using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("VisitAttachmentView", Schema = "HomeVisits")]
    public class VisitAttachmentView
    {
        [Column(Order = 0)]
        public Guid AttachmentId { get; set; }

        [Column(Order = 1)]
        public string FileName { get; set; }

        [Column(Order = 2)]
        public Guid VisitId { get; set; }
       
    }
}
