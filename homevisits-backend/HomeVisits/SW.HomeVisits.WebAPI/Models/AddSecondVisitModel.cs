using System;
using System.ComponentModel.DataAnnotations;
namespace SW.HomeVisits.WebAPI.Models
{
    public class AddSecondVisitModel
    {
        public Guid TimeZoneGeoZoneId { get; set; }
        public DateTime VisitDate { get; set; }
        public string RequiredTests { get; set; }
        public string Comments { get; set; }
        public Guid OriginVisitId { get; set; }
        public int SecondVisitReason { get; set; }
        public int? MinMinutes { get; set; }
        public int? MaxMinutes { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int? MobileBatteryPercentage { get; set; }
        public Guid? ChemistId { get; set; }
        public string VisitTime { get; set; }

    }
}
