using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistVisitsScheduleView), Schema = "HomeVisits")]
    public class ChemistVisitsScheduleView
    {
        [Column(Order = 0)]
        [Key]
        public Guid VisitId { get; set; }
        [Column(Order = 1)]
        [Key]
        public DateTime VisitDate { get; set; }
        [Column(Order = 2)]
        [Key]
        public int VisitCode { get; set; }
        [Column(Order = 3)]
        [Key]
        public Guid PatientId { get; set; }
        [Column(Order = 4)]
        [Key]
        public string PatientName { get; set; }
        [Column(Order = 5)]
        [Key]
        public string PatientNo { get; set; }
        [Column(Order = 6)]
        [Key]
        public string PhoneNumber { get; set; }
        [Column(Order = 7)]
        [Key]
        public Guid ChemistId { get; set; }
        [Column(Order = 8)]
        [Key]
        public int VisitStatusTypeId { get; set; }
        [Column(Order = 9)]
        [Key]
        public string StatusNameEn { get; set; }
        [Column(Order = 10)]
        [Key]
        public string StatusNameAr { get; set; }
        [Column(Order = 11)]
        [Key]
        public Guid PatientAddressId { get; set; }
        [Column(Order = 12)]
        [Key]
        public Guid GeoZoneId { get; set; }
        [Column(Order = 13)]
        [Key]
        public string GeoZoneNameEn { get; set; }
        [Column(Order = 14)]
        [Key]
        public string GeoZoneNameAr { get; set; }
        [Column(Order = 15)]
        [Key]
        public string Building { get; set; }
        [Column(Order = 16)]
        [Key]
        public string street { get; set; }
        [Column(Order = 17)]
        [Key]
        public string GoverNameEn { get; set; }
        [Column(Order = 18)]
        [Key]
        public string GoverNameAr { get; set; }
        [Column(Order = 19)]
        [Key]
        public Guid TimeZoneGeoZoneId { get; set; }
        [Column(Order = 20)]
        [Key]
        public TimeSpan StartTime { get; set; }
        [Column(Order = 21)]
        [Key]
        public TimeSpan EndTime { get; set; }
        [Column(Order = 22)]
        [Key]
        public TimeSpan? VisitTime { get; set; }
    }
}
