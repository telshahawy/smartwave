using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistsLastTrackingLogView), Schema = "HomeVisits")]
    public class ChemistsLastTrackingLogView
    {
        [Column(Order = 0)]
        [Key]
        public Guid ChemistId { get; set; }

        [Column(Order = 1)]
        [Key]
        public string Name { get; set; }
        [Column(Order = 2)]
        [Key]
        public string PhoneNumber { get; set; }
        [Column(Order = 3)]
        [Key]
        public float Latitude { get; set; }

        [Column(Order = 4)]
        [Key]
        public float Longitude { get; set; }
        [Column(Order = 5)]
        [Key]
        public int MobileBatteryPercentage { get; set; }
        [Column(Order = 6)]
        [Key]
        public DateTime CreationDate { get; set; }
        [Column(Order = 7)]
        [Key]
        public int VisitNo { get; set; }
        [Column(Order = 8)]
        [Key]
        public DateTime VisitDate { get; set; }
        [Column(Order = 9)]
        [Key]
        public TimeSpan? VisitTime { get; set; }
        [Column(Order = 10)]
        [Key]
        public string AreaNameAr { get; set; }
        [Column(Order = 11)]
        [Key]
        public string AreaNameEN { get; set; }

    }
}