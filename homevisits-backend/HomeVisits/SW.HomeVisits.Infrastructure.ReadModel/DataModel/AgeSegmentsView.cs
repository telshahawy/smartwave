using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(AgeSegmentsView), Schema = "HomeVisits")]
    public class AgeSegmentsView
    {
        [Column(Order = 0)]
        public Guid AgeSegmentId { get; set; }

        [Column(Order = 1)]
        public Guid ClientId { get; set; }

        [Column(Order = 2)]
        public string Name { get; set; }

        [Column(Order = 3)]
        public int AgeFromDay { get; set; }

        [Column(Order = 4)]
        public int AgeFromMonth { get; set; }

        [Column(Order = 5)]
        public int AgeFromYear { get; set; }

        [Column(Order = 6)]
        public int AgeToDay { get; set; }

        [Column(Order = 7)]
        public int AgeToMonth { get; set; }

        [Column(Order = 8)]
        public int AgeToYear { get; set; }

        [Column(Order = 9)]
        public bool IsActive { get; set; }

        [Column(Order = 10)]
        public bool IsDeleted { get; set; }

        [Column(Order = 11)]
        public bool NeedExpert { get; set; }

        [Column(Order = 12)]
        public bool AgeFromInclusive { get; set; }

        [Column(Order = 13)]
        public bool AgeToInclusive { get; set; }

        [Column(Order = 14)]
        public int Code { get; set; }

    }
}
