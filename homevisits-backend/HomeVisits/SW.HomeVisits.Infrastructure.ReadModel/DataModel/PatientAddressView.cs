using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(PatientAddressView), Schema = "HomeVisits")]
    public class PatientAddressView
    {
        [Column(Order = 0)]
        public Guid PatientAddressId { get; set; }

        [Column(Order = 1)]
        public string Latitude { get; set; }

        [Column(Order = 2)]
        public string Longitude { get; set; }

        [Column(Order = 3)]
        public string Floor { get; set; }

        [Column(Order = 4)]
        public string Flat { get; set; }

        [Column(Order = 5)]
        public Guid PatientId { get; set; }

        [Column(Order = 6)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 7)]
        public string Building { get; set; }

        [Column(Order = 8)]
        public string street { get; set; }

        [Column(Order = 9)]
        public bool IsConfirmed { get; set; }

        [Column(Order = 10)]
        public string LocationUrl { get; set; }

        [Column(Order = 11)]
        public string ZoneNameAr { get; set; }

        [Column(Order = 12)]
        public string ZoneNameEn { get; set; }

        [Column(Order = 13)]
        public string KmlFilePath { get; set; }

        [Column(Order = 14)]
        public Guid GovernateId { get; set; }

        [Column(Order = 15)]
        public string GoverNameEn { get; set; }

        [Column(Order = 16)]
        public string GoverNameAr { get; set; }

        [Column(Order = 17)]
        public Guid CountryId { get; set; }

        [Column(Order = 18)]
        public string CountryNameEn { get; set; }

        [Column(Order = 19)]
        public string CountryNameAr { get; set; }

        [Column(Order = 20)]
        public DateTime AddressCreatedAt { get; set; }
        [Column(Order = 21)]
        public int Code { get; set; }
    }
}
