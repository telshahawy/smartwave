using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistScheduleView), Schema = "HomeVisits")]
    public class ChemistScheduleView
    {
        [Column(Order = 0)]
        [Key]
        public Guid ChemistId { get; set; }

        [Column(Order = 1)]
        [Key]
        public string Name { get; set; }

        [Column(Order = 2)]
        [Key]
        public int Gender { get; set; }

        [Column(Order = 3)]
        [Key]
        public string DOB { get; set; }

        [Column(Order = 4)]
        [Key]
        public string PhoneNumber { get; set; }

        [Column(Order = 5)]
        [Key]
        public string street { get; set; }

        [Column(Order = 6)]
        [Key]
        public string Longitude { get; set; }

        [Column(Order = 7)]
        [Key]
        public string Latitude { get; set; }

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
        public string VisitNo { get; set; }

        [Column(Order = 12)]
        [Key]
        public DateTime VisitDate { get; set; }

        [Column(Order = 12)]
        [Key]
        public string ZoneNameAr { get; set; }

        [Column(Order = 13)]
        [Key]
        public string ZoneNameEn { get; set; }

        [Column(Order = 14)]
        [Key]
        public string GoverNameEn { get; set; }

        [Column(Order = 15)]
        [Key]
        public string GoverNameAr { get; set; }

        [Column(Order = 16)]
        [Key]
        public string Floor { get; set; }

        [Column(Order = 17)]
        [Key]
        public string Flat { get; set; }

        [Column(Order = 18)]
        [Key]
        public string Building { get; set; }

        [Column(Order = 19)]
        [Key]
        public Guid VisitId { get; set; }

        [Column(Order = 20)]
        [Key]
        public Guid PatientId { get; set; }

        [Column(Order = 21)]
        [Key]
        public bool ExpertChemist { get; set; }

        [Column(Order = 22)]
        [Key]
        public Guid ClientId { get; set; }
        [Column(Order = 23)]
        [Key]
        public Guid GeoZoneId { get; set; }
        [Column(Order = 24)]
        [Key]
        public Guid TimeZoneGeoZoneId { get; set; }
        [Column(Order = 25)]
        [Key]
        public float ChemistStartLatitude { get; set; }
        [Column(Order = 26)]
        [Key]
        public float ChemistStartLangitude { get; set; }
        [Column(Order = 27)]
        [Key]
        public TimeSpan TimeZoneStartTime { get; set; }
        [Column(Order = 28)]
        [Key]
        public TimeSpan TimeZoneEndTime { get; set; }
        [Key]
        [Column(Order = 29)]
        public int? VisitOrder { get; set; }
        [Key]
        [Column(Order = 30)]
        public float? VisitStartLatitude { get; set; }
        [Key]
        [Column(Order = 31)]
        public float? VisitStartLangitude { get; set; }
        [Key]
        [Column(Order = 32)]
        public float? VisitLatitude { get; set; }
        [Key]
        [Column(Order = 33)]
        public float? VisitLongitude { get; set; }
        [Key]
        [Column(Order = 34)]
        public int? VisitDistance { get; set; }
        [Key]
        [Column(Order = 35)]
        public int? VisitDuration { get; set; }
        [Key]
        [Column(Order = 36)]
        public int? VisitDurationInTraffic { get; set; }
    }
}
