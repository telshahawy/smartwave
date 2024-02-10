using System;
using System.Collections.Generic;
using System.Text;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class VisitStatus : Entity<Guid>
    {
        public VisitStatus(Guid visitStatusId, Guid visitId, float? longitude, float? latitude, string? deviceSerialNumber, int? mobileBatteryPercentage,
            int visitActionTypeId, int visitStatusTypeId, DateTime creationDate, int? actualNoOfPatients, int? noOfTests, bool? isAddressVerified, int? reasonId,
            string comments,Guid? createdBy)
        {
            VisitStatusId = visitStatusId;
            VisitId = visitId;
            Longitude = longitude;
            Latitude = latitude;
            DeviceSerialNumber = deviceSerialNumber;
            MobileBatteryPercentage = mobileBatteryPercentage;
            VisitActionTypeId = visitActionTypeId;
            VisitStatusTypeId = visitStatusTypeId;
            CreationDate = creationDate;
            ActualNoOfPatients = actualNoOfPatients;
            NoOfTests = noOfTests;
            IsAddressVerified = isAddressVerified;
            ReasonId = reasonId;
            Comments = comments;
            CreatedBy = createdBy;
        }

        public Guid VisitStatusId
        {
            get => Id;
            set => Id = value;
        }
        public Guid VisitId { get; set; }
        public Guid? CreatedBy { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string? DeviceSerialNumber { get; set; }
        public int? MobileBatteryPercentage { get; set; }
        public int VisitActionTypeId { get; set; }
        public int VisitStatusTypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public VisitStatusType VisitStatusType { get; set; }
        public Visit Visit { get; set; }
        public VisitActionType VisitActionType { get; set; }
        public int? ActualNoOfPatients { get; set; }
        public int? NoOfTests { get; set; }
        public bool? IsAddressVerified { get; set; }
        public int? ReasonId { get; set; }
        public Reason Reason { get; set; }
        public string Comments { get; set; }
    }
}