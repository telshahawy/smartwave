using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class VisitAttachmentsDto
    {
        public Guid AttachmentId { get; set; }
        public string FileName { get; set; }
        public Guid VisitId { get; set; }
    }
}
