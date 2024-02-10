using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddSecondVisitCommand
    {
        public Guid VisitId { get; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime VisitDate { get; set; }
        public string RequiredTests { get; set; }
        public string Comments { get; set; }

        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int? MobileBatteryPercentage { get; set; }

        public Guid OriginVisitId { get; set; }
        public int SecondVisitReason { get; set; }
        public int? MinMinutes { get; set; }
        public int? MaxMinutes { get; set; }
        public Guid? ChemistId { get; set; }
        public int SelectBy { get; set; }
    }
}
