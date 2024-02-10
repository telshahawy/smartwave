using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class VisitStatusesDto
    {
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public Guid VisitId { get; set; }
        public Guid VisitStatusId { get; set; }
        public int VisitStatusTypeId { get; set; }
    }
}
