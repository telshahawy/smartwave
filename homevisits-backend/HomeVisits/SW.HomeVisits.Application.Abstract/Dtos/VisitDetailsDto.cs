using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class VisitDetailsDto
    {
        public Guid VisitId { get; set; }
        public int VisitCode { get; set; }
        public string VisitNo { get; set; }
        public DateTime VisitDate { get; set; }
        public string TimeSlot { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public string RequiredTests { get; set; }
        public string Comments { get; set; }
        public Guid GeoZoneId { get; set; }
        public string ZoneName { get; set; }
        public int VisitStatusTypeId { get; set; }
        public string StatusName { get; set; }
        public string VisitDateString { get; set; }
        public string VisitTime { get; set; }
        public TimeSpan? VisitTimeValue { get; set; }
        public DateTime ActionCreationDate { get; set; }
        public int PlannedNoOfPatients { get; set; }
        public string PatientMobileNumber { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string ReservedBy { get; set; }
        public string Floor { get; set; }
        public string Flat { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string AddressFormatted { get; set; }
        public Guid PatientAddressId { get; set; }
        public string GoverName { get; set; }
        public IEnumerable<VisitAttachmentsDto> VisitAttachments { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public IEnumerable<VisitStatusesDto> VisitStatuses { get; set; }
        public int VisitTypeId { get; set; }
        public Guid? RelativeAgeSegmentId { get; set; }
        public Guid? ChemistId { get; set; }
        public string ChemistName { get; set; }
        public TimeSpan TimeZoneStartTime { get; set; }
        public TimeSpan TimeZoneEndTime { get; set; }
        public bool? IamNotSure { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
    }
}
