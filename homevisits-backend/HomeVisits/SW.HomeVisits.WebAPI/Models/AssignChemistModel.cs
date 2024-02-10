using System;
using System.ComponentModel.DataAnnotations;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AssignChemistModel
    {
        public int VisitTypeId { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public Guid GeoZoneId { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public DateTime VisitDate { get; set; }
        public Guid? OriginVisitId { get; set; }
        public Guid? ChemistId { get; set; }
        public Guid PatientId { get; set; }
        public Guid? RelativeAgeSegmentId { get; set; }

    }
}
