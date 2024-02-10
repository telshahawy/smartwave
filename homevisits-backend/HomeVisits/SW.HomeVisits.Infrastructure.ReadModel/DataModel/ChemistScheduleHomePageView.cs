using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table(nameof(ChemistScheduleHomePageView), Schema = "HomeVisits")]
    public class ChemistScheduleHomePageView
    {
        [Column(Order = 0)]
        public Guid ChemistId { get; set; }

        [Column(Order = 1)]
        public string Name { get; set; }

        [Column(Order = 2)]
        public int Gender { get; set; }

        [Column(Order = 3)]
        public string DOB { get; set; }

        [Column(Order = 4)]
        public string PhoneNumber { get; set; }

        [Column(Order = 5)]
        public string street { get; set; }

        [Column(Order = 6)]
        public string Longitude { get; set; }

        [Column(Order = 7)]
        public string Latitude { get; set; }

        [Column(Order = 8)]
        public int VisitStatusTypeId { get; set; }

        [Column(Order = 9)]
        public string StatusNameEn { get; set; }

        [Column(Order = 10)]
        public string StatusNameAr { get; set; }

        [Column(Order = 11)]
        public string VisitNo { get; set; }

        [Column(Order = 12)]
        public DateTime VisitDate { get; set; }

        [Column(Order = 12)]
        public Guid GeoZoneId { get; set; }
        
        [Column(Order = 13)]
        public string ZoneNameAr { get; set; }

        [Column(Order = 14)]
        public string ZoneNameEn { get; set; }

        [Column(Order = 15)]
        public string GoverNameEn { get; set; }

        [Column(Order = 16)]
        public string GoverNameAr { get; set; }

        [Column(Order = 17)]
        public string Floor { get; set; }

        [Column(Order = 18)]
        public string Flat { get; set; }

        [Column(Order = 19)]
        public string Building { get; set; }

        [Column(Order = 20)]
        public Guid VisitId { get; set; }

        [Column(Order = 21)]
        public Guid PatientId { get; set; }

        [Column(Order = 22)]
        public bool ExpertChemist { get; set; }

        [Column(Order = 23)]
        public Guid ClientId { get; set; }
    }
}
