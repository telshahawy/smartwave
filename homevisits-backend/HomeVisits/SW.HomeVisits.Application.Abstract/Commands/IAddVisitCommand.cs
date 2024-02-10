using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddVisitCommand
    {
        public Guid VisitId { get; }
        public string VisitNo { get;  }
        public int VisitTypeId { get;  }
        public DateTime VisitDate { get;  }
        public Guid PatientId { get; }
        public Guid PatientAddressId { get;  }
        public Guid? ChemistId { get; }
        public Guid CreatedBy { get; }
        public Guid? RelativeAgeSegmentId { get; set; }
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
        public bool? IamNotSure { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
        public List<CreateVisitAttachmentsDto> Attachments { get; set; }
    }
}
