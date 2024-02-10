using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("GeoZonesView", Schema = "HomeVisits")]
    public class GeoZoneView
    {
        [Column(Order = 0)]
        [Key]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 1)]
        [Key]
        public string KmlFilePath { get; set; }

        [Column(Order = 2)]
        [Key]
        public Guid governateId { get; set; }
        [Column(Order = 3)]
        [Key]
        public string NameAr { get; set; }
        [Column(Order = 4)]
        [Key]
        public string NameEn { get; set; }
        [Column(Order = 5)]
        [Key]
        public bool IsActive { get; set; }
        [Column(Order = 6)]
        [Key]
        public bool IsDeleted { get; set; }
        [Column(Order = 7)]
        [Key]
        public Guid? ClientId { get; set; }
        [Column(Order = 8)]
        [Key]
        public bool? GovernatIsActive { get; set; }
        [Column(Order = 9)]
        [Key]
        public bool? GovernatIsDeleted { get; set; }
        [Column(Order = 10)]
        [Key]
        public bool? CountryIsActive { get; set; }
        [Column(Order = 11)]
        [Key]
        public bool? CountryIsDeleted { get; set; }

        [Column(Order = 12)]
        [Key]
        public Guid CountryId { get; set; }

        [Column(Order = 13)]
        [Key]
        public string MappingCode { get; set; }

        [Column(Order = 14)]
        [Key]
        public int Code { get; set; }

        [Column(Order = 15)]
        [Key]
        public string GoverNameEn { get; set; }
        
        [Column(Order = 16)]
        [Key]
        public string KmlFileName { get; set; }

    }
}
