using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class UserGeoZone : Entity<Guid>
    {
        public Guid UserGeoZoneId { get; set; }
        public Guid UserId { get; set; }
        public Guid GeoZoneId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public GeoZone GeoZone { get; set; }
    }
}
