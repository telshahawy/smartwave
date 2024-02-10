using SW.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Domain.Entities
{
    public class ChemistVisitOrder : Entity<Guid>
    {
        public Guid ChemistVisitOrderId { get => Id; set => Id = value; }
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
        public Chemist Chemist { get; set; }
        public Visit Visit { get; set; }
        public TimeZoneFrame TimeZoneFrame { get; set; }
        public int Distance { get; set; }
        public int DurationInTraffic { get; set; }
        public int Duration { get; set; }       
    }
}
