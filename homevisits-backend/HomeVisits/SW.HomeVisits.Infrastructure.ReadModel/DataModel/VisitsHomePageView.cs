﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(VisitsHomePageView), Schema = "HomeVisits")]
    public class VisitsHomePageView
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
        public string PhoneNumber { get; set; }//patient

        [Column(Order = 11)]
        public Guid? ChemistId { get; set; }

        [Column(Order = 12)]
        public DateTime CreatedDate { get; set; }

        [Column(Order = 13)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 14)]
        public string GeoZoneNameEn { get; set; }

        [Column(Order = 15)]
        public string GeoZoneNameAr { get; set; }

        [Column(Order = 16)]
        public string ChemistName { get; set; }

        [Column(Order = 17)]
        public int? VisitActionTypeId { get; set; }

        //[Column(Order = 18)]
        //public int ActionNameEn { get; set; }

        //[Column(Order = 19)]
        //public int ActionNameAr { get; set; }

        [Column(Order = 18)]
        public int VisitStatusTypeId { get; set; }

        [Column(Order = 19)]
        public string StatusNameEn { get; set; }

        [Column(Order = 20)]
        public string StatusNameAr { get; set; }

        [Column(Order = 21)]
        public Guid CountryId { get; set; }

        [Column(Order = 22)]
        public string CountryNameEn { get; set; }

        [Column(Order = 23)]
        public string CountryNameAr { get; set; }

        [Column(Order = 24)]
        public Guid GovernateId { get; set; }

        [Column(Order = 25)]
        public string GoverNameEn { get; set; }

        [Column(Order = 26)]
        public string GoverNameAr { get; set; }

        [Column(Order = 27)]
        public bool? NeedExpert { get; set; }

        [Column(Order = 28)]
        public Guid PatientAddressId { get; set; }

        [Column(Order = 29)]
        public Guid TimeZoneGeoZoneId { get; set; }
        [Column(Order = 30)]
        public TimeSpan StartTime { get; set; }
        [Column(Order = 31)]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 32)]
        public string Latitude { get; set; }

        [Column(Order = 33)]
        public string Longitude { get; set; }
        [Column(Order = 34)]
        public int VisitCode { get; set; }
        [Column(Order = 35)]
        public Guid VisitStatusId { get; set; }
        [Column(Order = 36)]
        public DateTime VisitStatusCreationDate { get; set; }
    }
}
