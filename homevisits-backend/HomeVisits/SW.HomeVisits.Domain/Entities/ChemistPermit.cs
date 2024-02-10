using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class ChemistPermit:Entity<Guid>
    {
        public Guid ChemistPermitId { get; set; }
        public Guid ChemistId { get; set; }
        public DateTime PermitDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
