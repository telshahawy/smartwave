using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(LostVisitTimesView), Schema = "HomeVisits")]
    public class LostVisitTimesView
    {
        [Column(Order = 0)]
        public Guid LostVisitTimeId { get; set; }

        [Column(Order = 1)]
        public Guid CreatedBy { get; set; }

        [Column(Order = 2)]
        public Guid VisitId { get; set; }

        [Column(Order = 3)]
        public TimeSpan LostTime { get; set; }

        [Column(Order = 4)]
        public DateTime CreatedOn { get; set; }

        [Column(Order = 5)]
        public string VisitNo { get; set; }
        [Column(Order = 6)]
        public int VisitCode { get; set; }
        
    }
}
