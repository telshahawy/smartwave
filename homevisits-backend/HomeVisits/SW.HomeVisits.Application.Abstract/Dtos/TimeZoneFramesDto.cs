using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class TimeZoneFramesDto
    {
        public Guid TimeZoneFrameId { get; set; }

        public Guid GeoZoneId { get; set; }

        public string Name { get; set; }

        public int VisitsNoQuota { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool IsDeleted { get; set; }

        public bool BranchDispatch { get; set; }

        public TimeSpan StartTimeValue { get; set; }

        public TimeSpan EndTimeValue { get; set; }
    }
}
