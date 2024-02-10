using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Attachment : Entity<Guid>
    {
        public Guid AttachmentId { get => Id; set => Id = value; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Guid VisitId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}