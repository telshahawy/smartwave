using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Dtos
{
    public class UserAreasDto
    {
        public Guid GeoZoneId { get; set; }
        public string GeoZoneNameEn { get; set; }
        public string GeoZoneNameAr { get; set; }

    }
}
