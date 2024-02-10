using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(VisitDetailsReasonReportView), Schema = "HomeVisits")]
    public class VisitDetailsReasonReportView
    {
        [Column(Order = 0)]
        public Guid VisitId { get; set; }

        [Column(Order = 1)]
        public int ReasonId { get; set; }

        [Column(Order = 2)]
        public string CancelReason { get; set; }

        [Column(Order = 3)]
        public string VisitNo { get; set; }

        [Column(Order = 4)]
        public DateTime VisitDate { get; set; }

        [Column(Order = 5)]
        public Guid CreatedBy { get; set; }

        [Column(Order = 6)]
        public Guid PatientId { get; set; }

        [Column(Order = 7)]
        public string DOB { get; set; }

        [Column(Order = 8)]
        public int Gender { get; set; }

        [Column(Order = 9)]
        public string RequiredTests { get; set; }
        [Column(Order = 10)]
        public string Comments { get; set; }

        [Column(Order = 11)]
        public Guid CountryId { get; set; }
        [Column(Order = 12)]
        public string CountryNameEn { get; set; }
        [Column(Order = 13)]
        public string CountryNameAr { get; set; }

        [Column(Order = 14)]
        public Guid GovernateId { get; set; }

        [Column(Order = 15)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 16)]
        public string ZoneNameAr { get; set; }

        [Column(Order = 17)]
        public string ZoneNameEn { get; set; }
        [Column(Order = 18)]
        public int VisitActionTypeId { get; set; }
        [Column(Order = 19)]
        public string ActionNameEn { get; set; }
        [Column(Order = 20)]
        public string ActionNameAr { get; set; }

        [Column(Order = 18)]
        public int VisitStatusTypeId { get; set; }
        [Column(Order = 19)]
        public string StatusNameEn { get; set; }
        [Column(Order = 20)]
        public string StatusNameAr { get; set; }

        [Column(Order = 21)]
        public string Name { get; set; }

        [Column(Order = 22)]
        public DateTime ActionCreationDate { get; set; }

        [Column(Order = 23)]
        public int PlannedNoOfPatients { get; set; }

        [Column(Order = 24)]
        public float? Longitude { get; set; }

        [Column(Order = 25)]
        public float? Latitude { get; set; }

        [Column(Order = 26)]
        public int UserType { get; set; }

        [Column(Order = 27)]
        public string Floor { get; set; }

        [Column(Order = 28)]
        public string Flat { get; set; }

        [Column(Order = 29)]
        public string Building { get; set; }

        [Column(Order = 30)]
        public string street { get; set; }

        [Column(Order = 31)]
        public string GoverNameEn { get; set; }

        [Column(Order = 32)]
        public string GoverNameAr { get; set; }

        [Column(Order = 33)]
        public Guid TimeZoneGeoZoneId { get; set; }

        [Column(Order = 34)]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 35)]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 36)]
        public int VisitTypeId { get; set; }

        [Column(Order = 37)]
        public Guid? RelativeAgeSegmentId { get; set; }
        [Column(Order = 38)]
        public Guid PatientAddressId { get; set; }
        [Column(Order = 39)]
        public string PatientPhone { get; set; }

        [Column(Order = 40)]
        public string ChemistName { get; set; }

        [Column(Order = 41)]
        public Guid? ChemistId { get; set; }
        [Column(Order = 42)]
        public DateTime VisitStatusCreationDate { get; set; }
    }
}
