using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class CreateVisitAttachmentsDto
    {
        public Guid AttachmentId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Guid VisitId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
