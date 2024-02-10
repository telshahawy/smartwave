using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("ChemistTimeZoneAvailability", Schema = "HomeVisits")]
    public class ChemistTimeZoneAvailabilityView
    {
        [Key]
        public Guid GeoZoneId { get; set; }
        [Key]
        public string GeoZoneNameAe { get; set; }
        [Key]
        public string GeoZoneNameEn { get; set; }
        [Key]
        public string KmlFilePath { get; set; }
        [Key]
        public Guid GovernateId { get; set; }
        [Key]
        public Guid TimeZoneFrameId { get; set; }
        [Key]
        public int VisitsNoQouta { get; set; }
        [Key]
        public bool BranchDispatch { get; set; }
        [Key]
        public TimeSpan TimeZoneStartTime { get; set; }
        [Key]
        public TimeSpan TimeZoneEndTime { get; set; }
        [Key]
        public Guid ChemistId { get; set; }
        [Key]
        public TimeSpan ChemistStartTime { get; set; }
        [Key]
        public TimeSpan ChemistEndTime { get; set; }
        [Key]
        public int ChemistCode { get; set; }
        [Key]
        public Guid ChemistAssignedGeoZoneId { get; set; }
        [Key]
        public Guid ChemistScheduleId { get; set; }
        [Key]
        public DateTime ScheuleStartDate { get; set; }
        [Key]
        public DateTime ScheduleEndDate { get; set; }
        [Key]
        public float StartLatitude { get; set; }
        [Key]
        public float StartLangitude { get; set; }
        [Key]
        public Guid ChemistScheduleDayId { get; set; }
        [Key]
        public int Day { get; set; }
        [Key]
        public int ChemistSupportedTime { get; set; }
        
        [Key]
        public Guid ChemistClientId { get; set; }
        [Key]
        public string TimeZoneFrameNameAr { get; set; }
        [Key]
        public string TimeZoneFrameNameEn { get; set; }

        [Key]
        public bool ExpertChemist { get; set; }
    }
}
