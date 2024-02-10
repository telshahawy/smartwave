using System;
using SW.Framework.Domain;

namespace SW.HomeVisits.Domain.Entities
{
    public class RoleGeoZone : Entity<Guid>
    {
        public Guid RoleGeoZoneId { get => Id; set => Id = value; }
        public Guid RoleId { get; set; }
        public Guid GeoZoneId { get; set; }
        public bool IsDeleted { get; set; }
        public GeoZone GeoZone { get; set; }
    }
}
