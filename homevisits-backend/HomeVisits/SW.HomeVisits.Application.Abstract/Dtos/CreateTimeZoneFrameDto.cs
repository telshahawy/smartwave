using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class CreateTimeZoneFrameDto
    {
        public Guid TimeZoneFrameId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int VisitsNoQuota { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool BranchDispatch { get; set; }
        public Guid GeoZoneId { get; set; }
    }
}
