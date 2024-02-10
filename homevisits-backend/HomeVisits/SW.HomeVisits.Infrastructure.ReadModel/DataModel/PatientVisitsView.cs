using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(PatientVisitsView), Schema = "HomeVisits")]
    public class PatientVisitsView
    {
        [Column(Order = 0)]
        public Guid VisitId { get; set; }

        [Column(Order = 1)]
        public string VisitNo { get; set; }

        [Column(Order = 2)]
        public DateTime VisitDate { get; set; }

        [Column(Order = 3)]
        public Guid GeoZoneId { get; set; }

        [Column(Order = 4)]
        public string ZoneNameAr { get; set; }

        [Column(Order = 5)]
        public string ZoneNameEn { get; set; }
        [Column(Order = 6)]
        public int VisitStatusTypeId { get; set; }
        [Column(Order = 7)]
        public string StatusNameEn { get; set; }
        [Column(Order = 8)]
        public string StatusNameAr { get; set; }

        [Column(Order = 9)]
        public Guid PatientId { get; set; }
        [Column(Order = 10)]
        public int VisitCode { get; set; }
    }
}
