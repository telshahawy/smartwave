using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistPermitsView), Schema = "HomeVisits")]
    public class ChemistPermitsView
    {
        [Key]
        public Guid ChemistPermitId { get; set; }
        public Guid ChemistId { get; set; }
        public DateTime PermitDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public Guid ClientId { get; set; }
    }
}
