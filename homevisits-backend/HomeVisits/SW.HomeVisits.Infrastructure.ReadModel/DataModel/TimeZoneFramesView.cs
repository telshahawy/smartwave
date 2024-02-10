using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(TimeZoneFramesView), Schema = "HomeVisits")]
    public class TimeZoneFramesView
    {
        [Column(Order = 0)]
        public Guid TimeZoneFrameId { get; set; }

        [Column(Order = 1)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 2)]
        public string NameAr { get; set; }

        [Column(Order = 3)]
        public string NameEN { get; set; }

        [Column(Order = 4)]
        public int VisitsNoQouta { get; set; }

        [Column(Order = 5)]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 6)]
        public TimeSpan EndTime { get; set; }

        [Column(Order = 7)]
        public bool IsDeleted { get; set; }

        [Column(Order = 8)]
        public bool BranchDispatch { get; set; }

    }
}
