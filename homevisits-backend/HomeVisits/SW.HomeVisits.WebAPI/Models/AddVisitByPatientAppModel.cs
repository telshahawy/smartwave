using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddVisitByPatientAppModel
    {
        public int VisitTypeId { get; set; }
        public DateTime VisitDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientAddressId { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public int PlannedNoOfPatients { get; set; }
        public string RequiredTests { get; set; }
        public string Comments { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int MobileBatteryPercentage { get; set; }
        public Guid? ChemistId { get; set; }
        public Guid? ClientId { get; set; }
        public string VisitTime { get; set; }
        public string RelativeName { get; set; }
        public int? RelativeGender { get; set; }
        public string RelativePhoneNumber { get; set; }
        public Guid? RelativeAgeSegmentId { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
        public List<IFormFile> Atachments { get; set; }
    }
}
