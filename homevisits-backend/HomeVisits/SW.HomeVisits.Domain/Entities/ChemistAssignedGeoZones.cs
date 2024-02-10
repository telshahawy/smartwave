using System;
using SW.Framework.Domain;
using System.Collections.Generic;

namespace SW.HomeVisits.Domain.Entities
{
    public class ChemistAssignedGeoZone:Entity<Guid>
    {
        public Guid ChemistAssignedGeoZoneId { get; set; }
        public Guid ChemistId { get; set; }
        public Guid GeoZoneId { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ChemistSchedule> Schedule { get; set; }
        public GeoZone GeoZone { get; set; }
    }
}
