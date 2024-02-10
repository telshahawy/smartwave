using System;
using SW.Framework.Domain;
using System.Collections.Generic;

namespace SW.HomeVisits.Domain.Entities
{
    public class ChemistSchedule:Entity<Guid>
    {
        public Guid ChemistScheduleId { get; set; }
        public Guid ChemistAssignedGeoZoneId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float StartLatitude { get; set; }
        public float StartLangitude{  get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ChemistScheduleDay> ScheduleDays { get; set; }

    }
}
