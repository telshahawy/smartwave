using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Models
{
    public class GetGeoZoneForEditQuery : IGetGeoZoneForEditQuery
    {
        public Guid GeoZoneId { get; set; }
    }
}
