using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateTimeZoneFrameModel
    {
        public Guid TimeZoneFrameId { get; set; }
        public string Name { get; set; }
        public int VisitsNoQuota { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool BranchDispatch { get; set; }
        public Guid GeoZoneId { get; set; }
    }
}
