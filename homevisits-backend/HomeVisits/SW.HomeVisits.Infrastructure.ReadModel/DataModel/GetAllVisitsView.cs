using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(GetAllVisitsView), Schema = "HomeVisits")]
    public class GetAllVisitsView
    {
        [Key]
        public Guid VisitId { get; set; }
        [Key]
        public string VisitNo { get; set; }
        [Key]
        public int VisitTypeId { get; set; }
        [Key]
        public DateTime VisitDate { get; set; }
        [Key]
        public Guid PatientId { get; set; }
        [Key]
        public Guid PatientAddressId { get; set; }
        [Key]
        public Guid? ChemistId { get; set; }
        [Key]
        public Guid CreatedBy { get; set; }
        [Key]
        public DateTime CreatedDate { get; set; }
        [Key]
        public Guid? RelativeAgeSegmentId { get; set; }
        [Key]
        public Guid TimeZoneGeoZoneId { get; set; }

        [Key]
        public Guid? VisitStatusId { get; set; }

        [Key]
        public int? VisitStatusTypeId { get; set; }

        [Key]
        public Guid ClientId { get; set; }
        [Key]
        public Guid TimeZoneFrameId { get; set; }
        [Key]
        public int VisitStatus { get; set; }
        public int PlannedNoOfPatients { get; set; }

        [Key]
        public Guid GeoZoneId { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public int VisitCode { get; set; }
        public bool? IamNotSure { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
    }
}
