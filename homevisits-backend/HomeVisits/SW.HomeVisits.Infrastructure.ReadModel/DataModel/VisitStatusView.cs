using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(VisitStatusView), Schema = "HomeVisits")]
    public class VisitStatusView
    {
        [Column(Order = 0)]
        public Guid VisitId { get; set; }

        [Column(Order = 1)]
        public DateTime CreationDate { get; set; }

        [Column(Order = 2)]
        public float? Longitude { get; set; }

        [Column(Order = 3)]
        public float? Latitude { get; set; }

        [Column(Order = 4)]
        public Guid VisitStatusId { get; set; }

        [Column(Order = 5)]
        public int VisitStatusTypeId { get; set; }

        [Column(Order = 6)]
        public string StatusNameEn { get; set; }

        [Column(Order = 7)]
        public string StatusNameAr { get; set; }

        [Column(Order = 8)]
        public string UserName { get; set; }

        [Column(Order = 9)]
        public Guid ClientId { get; set; }
        [Column(Order = 10)]
        public Guid CreatedBy { get; set; }
    }
}
