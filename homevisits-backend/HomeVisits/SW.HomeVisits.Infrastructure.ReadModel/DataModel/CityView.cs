using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(CityView), Schema = "HomeVisits")]
    public class CityView
    {

        [Column(Order = 0)]
        [Key]
        public Guid CityId { get; set; }

        [Column(Order = 1)]
        [Key]
        public string NameAr { get; set; }

        [Column(Order =2)]
        [Key]
        public string NameEn { get; set; }
    }
}
