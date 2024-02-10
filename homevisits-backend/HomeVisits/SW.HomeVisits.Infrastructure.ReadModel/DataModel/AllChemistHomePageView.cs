using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("AllChemistHomePageView", Schema = "HomeVisits")]
    public class AllChemistHomePageView
    {
        [Column(Order = 0)]
        public Guid ChemistId { get; set; }

         [Column(Order = 1)]
        public string ChemistName { get; set; }

         [Column(Order = 2)]
        public Guid GeoZoneId { get; set; }

         [Column(Order = 3)]
        public int Day { get; set; }

         [Column(Order = 4)]
        public DateTime StartDate { get; set; }

         [Column(Order = 5)]
        public DateTime EndDate { get; set; }

         [Column(Order = 6)]
        public TimeSpan StartTime { get; set; }

        [Column(Order = 7)]
        public TimeSpan EndTime { get; set; }

        
    }
}
