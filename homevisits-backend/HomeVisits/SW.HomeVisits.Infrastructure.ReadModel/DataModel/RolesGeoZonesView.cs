using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(RolesGeoZonesView), Schema = "HomeVisits")]
    public class RolesGeoZonesView
    {
        [Key]
        public Guid RoleGeoZoneId { get; set; }
        public Guid RoleId { get; set; }
        public Guid GeoZoneId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
