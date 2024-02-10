using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateVisitStatusCommand
    {
        public Guid VisitStatusId { get; set; }
        public Guid VisitId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid CreatedBy { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int? MobileBatteryPercentage { get; set; }
        public int VisitActionTypeId { get; set; }
        public int VisitStatusTypeId { get; set; }
        public int? ActualNoOfPatients { get; set; }
        public int? NoOfTests { get; set; }
        public bool? IsAddressVerified { get; set; }
        public int? ReasonId { get; set; }
        public string Comments { get; set; }
        public Guid? TimeZoneGeoZoneId { get; set; }
        public DateTime? VisitDate { get; set; }
        public Guid? ChemistId { get; set; }

    }
}
