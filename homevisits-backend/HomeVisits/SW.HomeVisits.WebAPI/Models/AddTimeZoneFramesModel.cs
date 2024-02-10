using System;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddTimeZoneFramesModel
    {
        public string TimeZoneFrameName { get; set; }
        public int VisitsNoQuota { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool BranchDispatch { get; set; }
    }
}
