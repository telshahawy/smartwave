using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.WebAPI.Models
{
    public class AddVisitByPatientCommand : IAddVisitByPatientCommand
    {
        public Guid VisitId { get; set; }

        public string VisitNo { get; set; }

        public int VisitTypeId { get; set; }

        public DateTime VisitDate { get; set; }

        public Guid PatientId { get; set; }

        public Guid PatientAddressId { get; set; }

        public Guid? ChemistId { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid TimeZoneGeoZoneId { get; set; }
        public int PlannedNoOfPatients { get; set; }
        public string RequiredTests { get; set; }
        public string Comments { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int? MobileBatteryPercentage { get; set; }
        public int SelectBy { get; set; }
        public string VisitTime { get; set; }
        public string RelativeName { get; set; }
        public int? RelativeGender { get; set; }
        public string RelativePhoneNumber { get; set; }
        public Guid? RelativeAgeSegmentId { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
        public List<CreateVisitAttachmentsDto> Attachments { get; set; }
    }
}
