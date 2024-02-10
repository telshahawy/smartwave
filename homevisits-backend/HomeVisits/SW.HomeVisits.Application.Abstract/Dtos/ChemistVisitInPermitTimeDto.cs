using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistVisitInPermitTimeDto
    {
        public Guid VisitId { get; set; }
        public int VisitNo { get; set; }
        public int VisitCode { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public string TimeZoneTimeSlot { get; set; }
        public string TimeZoneStartTimeString { get; set; }
        public string TimeZoneEndTimeString { get; set; }
        public TimeSpan TimeZoneStartTime { get; set; }
        public TimeSpan TimeZoneEndTime { get; set; }
    }
}
