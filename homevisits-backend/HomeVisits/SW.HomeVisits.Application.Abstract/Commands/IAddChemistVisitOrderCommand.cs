using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IAddChemistVisitOrderCommand
    {
        public Guid ChemistVisitOrderId { get; set; }
        public Guid VisitId { get; set; }
        public Guid ChemistId { get; set; }
        public Guid TimeZoneFrameId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int VisitOrder { get; set; }
        public float StartLatitude { get; set; }
        public float StartLangitude { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public int DurationInTraffic { get; set; }
    }
}
