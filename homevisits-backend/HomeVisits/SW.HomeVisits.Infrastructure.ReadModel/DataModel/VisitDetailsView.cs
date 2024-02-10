using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(VisitDetailsView), Schema = "HomeVisits")]
    public class VisitDetailsView
    {
        [Column(Order = 0)]
        public Guid VisitId { get; set; }

        [Column(Order = 1)]
        public string VisitNo { get; set; }

        [Column(Order = 2)]
        public DateTime VisitDate { get; set; }

        [Column(Order = 3)]
        public Guid CreatedBy { get; set; }

        [Column(Order = 4)]
        public Guid PatientId { get; set; }

        [Column(Order = 5)]
        public string DOB { get; set; }
        
        [Column(Order = 6)]
        public int Gender { get; set; }

        [Column(Order = 7)]
        public string RequiredTests { get; set; }

        [Column(Order = 8)]
        public string Comments { get; set; }

        [Column(Order = 9)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 10)]
        public string ZoneNameAr { get; set; }

        [Column(Order = 11)]
        public string ZoneNameEn { get; set; }
        [Column(Order = 12)]
        public int VisitStatusTypeId { get; set; }
        [Column(Order = 13)]
        public string StatusNameEn { get; set; }
        [Column(Order = 14)]
        public string StatusNameAr { get; set; }

        [Column(Order = 15)]
        public string Name { get; set; }

        [Column(Order = 16)]
        public DateTime ActionCreationDate { get; set; }

        [Column(Order = 17)]
        public int PlannedNoOfPatients { get; set; }

        [Column(Order = 18)]
        public float? Longitude { get; set; }

        [Column(Order = 19)]
        public float? Latitude { get; set; }

        [Column(Order = 19)]
        public int? UserType { get; set; }

        [Column(Order = 20)]
        public string Floor { get; set; }

        [Column(Order = 21)]
        public string Flat { get; set; }

        [Column(Order = 22)]
        public string Building { get; set; }

        [Column(Order = 23)]
        public string street { get; set; }

        [Column(Order = 24)]
        public string GoverNameEn { get; set; }

        [Column(Order = 25)]
        public string GoverNameAr { get; set; }

        [Column(Order = 26)]
        public Guid TimeZoneGeoZoneId { get; set; }

        [Column(Order = 27)]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 28)]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 29)]
        public int VisitTypeId { get; set; }

        [Column(Order = 30)]
        public Guid? RelativeAgeSegmentId { get; set; }
        [Column(Order = 31)]
        public Guid PatientAddressId { get; set; }
         [Column(Order = 32)]
        public int VisitCode { get; set; }
         [Column(Order = 33)]
        public Guid? ChemistId { get; set; }
         [Column(Order = 34)]
        public string ChemistName { get; set; }
        [Column(Order = 35)]
        public Guid StatusCreatedBy { get; set; }
        [Column(Order = 36)]
        public TimeSpan? VisitTime { get; set; }
        [Column(Order = 37)]
        public bool? IamNotSure { get; set; }
        [Column(Order = 38)]
        public DateTime? RelativeDateOfBirth { get; set; }
    }
}
