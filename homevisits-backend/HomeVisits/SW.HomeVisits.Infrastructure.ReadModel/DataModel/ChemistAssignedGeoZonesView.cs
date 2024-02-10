using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistAssignedGeoZonesView), Schema = "HomeVisits")]
    public class ChemistAssignedGeoZonesView
    {
        [Column(Order = 0)]
        [Key]
        public Guid GeoZoneId { get; set; }
        [Column(Order = 1)]
        [Key]
        public string NameAr { get; set; }
        [Column(Order = 2)]
        [Key]
        public string NameEn { get; set; }
        [Column(Order = 3)]
        [Key]
        public string KmlFilePath { get; set; }
        [Column(Order = 4)]
        [Key]
        public Guid GovernateId { get; set; }
        [Column(Order = 5)]
        [Key]
        public Guid ChemistId { get; set; }

        [Column(Order = 6)]
        [Key]
        public string ChemistName { get; set; }

        [Column(Order = 7)]
        [Key]
        public Guid ClientId { get; set; }

        [Column(Order = 8)]
        [Key]
        public Guid ChemistAssignedGeoZoneId { get; set; }

        [Column(Order = 9)]
        [Key]
        public bool IsActive { get; set; }
        [Column(Order = 10)]
        [Key]
        public bool IsDeleted { get; set; }

    }
}
