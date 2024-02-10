using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("CountriesView", Schema = "HomeVisits")]
    public class CountryView
    {
        [Column(Order = 0)]
        [Key]
        public Guid CountryId { get; set; }

        [Column(Order = 1)]
        [Key]
        public string CountryNameAr { get; set; }

        [Column(Order = 2)]
        [Key]
        public string CountryNameEn { get; set; }
        [Column(Order = 3)]
        [Key]
        public Guid? ClientId { get; set; }
        [Column(Order = 4)]
        [Key]
        public bool IsActive { get; set; }
        [Column(Order = 5)]
        [Key]
        public bool IsDeleted { get; set; }

        [Column(Order = 6)]
        [Key]
        public int Code { get; set; }

        [Column(Order = 7)]
        [Key]
        public int MobileNumberLength { get; set; }

    }
}
