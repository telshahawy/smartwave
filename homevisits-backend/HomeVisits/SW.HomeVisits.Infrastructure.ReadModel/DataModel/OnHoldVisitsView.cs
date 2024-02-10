using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(OnHoldVisitsView), Schema = "HomeVisits")]
    public class OnHoldVisitsView
    {
        [Column(Order = 0)]
        public Guid OnHoldVisitId { get; set; }
        [Column(Order = 1)]
        public Guid ChemistId { get; set; }
        [Column(Order = 2)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 3)]
        public Boolean IsCanceled { get; set; }
        [Column(Order = 4)]
        public Guid TimeZoneFrameId { get; set; }
        [Column(Order = 5)]
        public string DeviceSerialNo { get; set; }
    }
}
