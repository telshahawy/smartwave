using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class AvailableVisitsInAreaDto
    {

        public string GeoZoneName { get; set; }
        public Guid GeoZoneId { get; set; }
        public Guid VisitId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? AgeSegmentId { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitType { get; set; }
        public int AvalailableVisits { get; set; }
        public int MaxVisits { get; set; }
        public string TimeZoneName { get; set; }
        public string TimeZoneStartTime { get; set; }
        public string TimeZoneEndTime { get; set; }
        public Guid TimeZoneFrameGeoZoneId { get; set; }
        public TimeSpan StartTimeSpan { get; set; }
        public TimeSpan EndTimeSpan { get; set; }

    }
}
