﻿using System;
namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class ChemistVisitsOrderDto
    {
        public Guid ChemistVisitOrderId { get; set; }
        public Guid VisitId { get; set; }
        public Guid ChemistId { get; set; }
        public Guid TimeZoneFrameId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public float StartLatitude { get; set; }
        public float StartLangitude { get; set; }
        public int VisitOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public int VisitNo { get; set; }
        public int VisitCode { get; set; }
        public string VisitDate { get; set; }
        public string PatientName { get; set; }
        public string PatientNo { get; set; }
        public int Gender { get; set; }
        public string GenderName { get; set; }
        public string DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string GeoZoneName { get; set; }
        public string ChemistName { get; set; }
        public string StatusName { get; set; }
        public Guid GeoZoneId { get; set; }
        public string TimeSlot { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public TimeSpan TimeZoneStartTime { get; set; }
        public TimeSpan TimeZoneEndTime { get; set; }
        public DateTime VisitDateValue { get; set; }
        public Guid ClientId { get; set; }
        public int VisitStatusTypeId { get; set; }
        public int VisitTypeId { get; set; }
        public int VisitsNoQouta { get; set; }
        public int Distance { get; set; }
        public int DurationInTraffic { get; set; }
        public int Duration { get; set; }
    }
}