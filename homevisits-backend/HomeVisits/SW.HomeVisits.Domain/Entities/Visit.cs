using System;
using System.Collections.Generic;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class Visit : Entity<Guid>
    {
        public Guid VisitId { get => Id; set => Id = value; }
        public string VisitNo { get; set; }
        public int VisitTypeId { get; set; }
        public int VisitCode{ get; set; }
        public DateTime VisitDate { get; set; }
        public Guid PatientId { get; set; }
        public Guid PatientAddressId { get; set; }
        public Guid? ChemistId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? RelativeAgeSegmentId { get; set; }
        public bool? IamNotSure { get; set; }
        public DateTime? RelativeDateOfBirth { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public PatientAddress PatientAddress { get; set; }
        public Chemist Chemist { get; set; }
        public AgeSegment RelativeAgeSegment { get; set; }
        public VisitType VisitType { get; set; }
        public Patient Patient { get; set; }
        public TimeZoneFrame timeZoneFrame { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<VisitStatus> VisitStatuses { get; set; }
        public ICollection<VisitAction> VisitActions { get; set; }
        public int PlannedNoOfPatients { get; set; }
        public string RequiredTests { get; set; }
        public string Comments { get; set; }

        public Guid? OriginVisitId { get; set; }

        public Visit OriginVisit { get; set; }

        public int? MinMinutes { get; set; }
        public int? MaxMinutes { get; set; }
        public int SelectBy { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public string RelativeName { get; set; }
        public int? RelativeGender { get; set; }
        public string RelativePhoneNumber { get; set; }
    }
}