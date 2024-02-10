using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistTrackingLogView), Schema = "HomeVisits")]
    public class ChemistTrackingLogView
    {
        [Column(Order = 0)]
        public Guid ChemistTrackingLogId { get; set; }

        [Column(Order = 1)]
        public Guid ChemistId { get; set; }

        [Column(Order = 2)]
        public DateTime CreationDate { get; set; }

        [Column(Order = 3)]
        public float Longitude { get; set; }

        [Column(Order = 4)]
        public float Latitude { get; set; }

        [Column(Order = 5)]
        public string DeviceSerialNumber { get; set; }

        [Column(Order = 6)]
        public int MobileBatteryPercentage { get; set; }

        [Column(Order = 7)]
        public string UserName { get; set; }

    }
}
