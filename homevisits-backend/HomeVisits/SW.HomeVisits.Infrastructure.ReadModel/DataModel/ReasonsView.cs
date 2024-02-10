using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ReasonsView), Schema = "HomeVisits")]
    public class ReasonsView
    {

        [Column(Order = 0)]
        [Key]
        public int ReasonId { get; set; }

        [Column(Order = 1)]
        public string ReasonName { get; set; }

        [Column(Order = 2)]
        public int VisitTypeActionId { get; set; }
        [Column(Order = 3)]
        public bool IsActive { get; set; }
        [Column(Order = 4)]
        public bool IsDeleted { get; set; }
        [Column(Order = 5)]
        public Guid ClientId { get; set; }

        [Column(Order = 6)]
        public int? ReasonActionId { get; set; }

        [Column(Order = 7)]
        public string ReasonActionName { get; set; }

    }
}
