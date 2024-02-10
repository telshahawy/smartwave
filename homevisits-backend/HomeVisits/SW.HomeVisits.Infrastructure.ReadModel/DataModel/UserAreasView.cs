using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.DataModel
{
    [Table("UserAreasView", Schema = "HomeVisits")]
    public class UserAreasView
    {
        [Column(Order = 0)]
        
        public Guid GeoZoneId { get; set; }

        [Column(Order = 1)]
        
        public string NameAr { get; set; }

        [Column(Order = 2)]
        
        public string NameEn { get; set; }

        [Column(Order = 3)]
        
        public Guid UserId { get; set; }

        [Column(Order = 4)]
        
        public bool GeoDeleted { get; set; }

        [Column(Order = 5)]
        
        public bool GeoActive { get; set; }

        [Column(Order = 6)]
        
        public bool UserGeoDeleted { get; set; }

        [Column(Order = 7)]
        
        public bool UserGeoActive { get; set; }


    }
}
