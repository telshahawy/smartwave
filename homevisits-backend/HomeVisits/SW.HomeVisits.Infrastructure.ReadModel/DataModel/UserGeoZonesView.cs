using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(UserGeoZonesView), Schema = "HomeVisits")]
    public class UserGeoZonesView
    {
        [Key]
        public Guid UserGeoZoneId { get; set; }
        public Guid UserId { get; set; }
        public Guid GeoZoneId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
