using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class TimeZoneFrame : Entity<Guid>
    {
        public Guid TimeZoneFrameId { get => Id; set => Id = value; }
        public Guid GeoZoneId { get; set; }
        public string NameAr { get; set; }
        public string NameEN { get; set; }
        public int VisitsNoQouta { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool BranchDispatch { get; set; }
    }
}

 