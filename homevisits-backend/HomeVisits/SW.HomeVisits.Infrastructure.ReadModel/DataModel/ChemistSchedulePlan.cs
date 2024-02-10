using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistSchedulePlan), Schema = "HomeVisits")]
    public class ChemistSchedulePlan
    {
        [Column(Order = 0)]
        public Guid ChemistId { get; set; }

        [Column(Order = 1)]
        public int Code { get; set; }

        [Column(Order = 2)]
        public int DOB { get; set; }

        [Column(Order = 3)]
        public bool ExpertChemist { get; set; }

        [Column(Order = 4)]
        public DateTime JoinDate { get; set; }

        [Column(Order = 5)]
        public Guid ChemistAssignedGeoZoneId { get; set; }

        [Column(Order = 6)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 7)]
        public Guid ChemistScheduleId { get; set; }

        [Column(Order = 8)]
        public DateTime ScheuleStartDate { get; set; }

        [Column(Order = 9)]
        public DateTime ScheduleEndDate { get; set; }

        [Column(Order = 10)]
        public float StartLatitude { get; set; }

        [Column(Order = 11)]
        public float StartLangitude { get; set; }

        [Column(Order = 12)]
        public Guid ChemistScheduleDayId { get; set; }

        [Column(Order = 13)]
        public int Day { get; set; }

        [Column(Order = 14)]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 15)]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 16)]
        public Guid ClientId { get; set; }

        [Column(Order = 17)]
        public bool AssignedZoneIsActive { get; set; }

        [Column(Order = 18)]
        public bool AssignedZoneIsDeleted { get; set; }

        [Column(Order = 19)]
        public bool ScheduleIsActive { get; set; }

        [Column(Order = 20)]
        public bool ScheduleIsDeleted { get; set; }

        [Column(Order = 21)]
        public bool ScheduleDaysIsDeleted { get; set; }

        [Column(Order = 22)]
        public string GeoZoneNameAr { get; set; }

        [Column(Order = 23)]
        public string GeoZoneNameEn { get; set; }

    }
}
