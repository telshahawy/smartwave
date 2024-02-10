using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class ChemistScheduleDay:Entity<Guid>
    {
        public Guid ChemistScheduleDayId { get =>Id; set => Id=value; }
        public Guid ChemistScheduleId { get; set; }
        public int Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
