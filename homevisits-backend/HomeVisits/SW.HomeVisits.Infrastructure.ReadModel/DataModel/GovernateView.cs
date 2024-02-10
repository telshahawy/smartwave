using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("GovernatsView", Schema = "HomeVisits")]
    public class GovernateView
    {
        [Column(Order = 0)]
        [Key]
        public Guid GovernateId { get; set; }

        [Column(Order = 1)]
        [Key]
        public string GoverNameAr { get; set; }

        [Column(Order = 2)]
        [Key]
        public string GoverNameEn { get; set; }
        [Column(Order = 3)]
        [Key]
        public Guid CountryId { get; set; }
        [Column(Order = 4)]
        [Key]
        public bool IsActive { get; set; }
        [Column(Order = 5)]
        [Key]
        public bool IsDeleted { get; set; }
        [Column(Order = 6)]
        [Key]
        public Guid? ClientId { get; set; }
        [Column(Order = 7)]
        [Key]
        public bool? CountryIsActive { get; set; }
        [Column(Order = 8)]
        [Key]
        public bool? CountryIsDeleted { get; set; }

        [Column(Order = 9)]
        [Key]
        public string CountryNameEn { get; set; }

        [Column(Order = 10)]
        [Key]
        public int Code { get; set; }

        [Column(Order = 11)]
        [Key]
        public string CustomerServiceEmail { get; set; }

    }
}

