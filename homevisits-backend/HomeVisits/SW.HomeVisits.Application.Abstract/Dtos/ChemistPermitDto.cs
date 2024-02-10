using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistPermitDto
    {
        public Guid ChemistPermitId { get; set; }
        public Guid ChemistId { get; set; }
        public DateTime PermitDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
