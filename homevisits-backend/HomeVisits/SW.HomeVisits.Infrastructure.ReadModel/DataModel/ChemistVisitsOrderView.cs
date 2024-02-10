using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistVisitsOrderView), Schema = "HomeVisits")]
    public class ChemistVisitsOrderView
    {
        [Column(Order = 0)]
        public Guid VisitId { get; set; }

        [Column(Order = 1)]
        public int VisitNo { get; set; }

        [Column(Order = 2)]
        public int VisitTypeId { get; set; }

        [Column(Order = 3)]
        public DateTime VisitDate { get; set; }

        [Column(Order = 4)]
        public Guid PatientId { get; set; }

        [Column(Order = 5)]
        public string PatientName { get; set; }

        [Column(Order = 6)]
        public string PatientNo { get; set; }

        [Column(Order = 7)]
        public int Gender { get; set; }

        [Column(Order = 8)]
        public string DOB { get; set; }

        [Column(Order = 9)]
        public Guid ClientId { get; set; }

        [Column(Order = 10)]
        public string PhoneNumber { get; set; }

        [Column(Order = 11)]
        public Guid ChemistId { get; set; }

        [Column(Order = 12)]
        public DateTime VisitCreatedDate { get; set; }

        [Column(Order = 13)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 14)]
        public string GeoZoneNameEn { get; set; }

        [Column(Order = 15)]
        public string GeoZoneNameAr { get; set; }

        [Column(Order = 16)]
        public string ChemistName { get; set; }

        [Column(Order = 17)]
        public int VisitStatusTypeId { get; set; }

        [Column(Order = 18)]
        public string StatusNameEn { get; set; }

        [Column(Order = 19)]
        public string StatusNameAr { get; set; }

        [Column(Order = 20)]
        public Guid GovernateId { get; set; }

        [Column(Order = 21)]
        public bool? NeedExpert { get; set; }
        [Column(Order = 22)]
        public Guid PatientAddressId{ get; set; }
        [Column(Order = 23)]
        public string Latitude { get; set; }
        [Column(Order = 24)]
        public string Longitude { get; set; }
        [Column(Order = 25)]
        public Guid TimeZoneGeoZoneId { get; set; }
         [Column(Order = 26)]
        public int VisitCode { get; set; }
        [Column(Order = 27)]
        public TimeSpan StartTime { get; set; }
        [Column(Order = 28)]
        public TimeSpan EndTime { get; set; }
        [Column(Order = 29)]
        public int VisitsNoQouta { get; set; }
        [Column(Order = 30)]
        public Guid ChemistVisitOrderId { get; set; }
        [Column(Order = 31)]
        public float StartLatitude { get; set; }
        [Column(Order = 32)]
        public float StartLangitude { get; set; }
        [Column(Order = 33)]
        public int VisitOrder { get; set; }
        [Column(Order = 34)]
        public DateTime VisitOrderCreatedDate { get; set; }
        [Column(Order = 35)]
        public bool IsDeleted { get; set; }
        [Column(Order = 36)]
        public int Distance { get; set; }
        [Column(Order = 37)]
        public int DurationInTraffic { get; set; }
        [Column(Order = 38)]
        public int Duration { get; set; }
    }
}
