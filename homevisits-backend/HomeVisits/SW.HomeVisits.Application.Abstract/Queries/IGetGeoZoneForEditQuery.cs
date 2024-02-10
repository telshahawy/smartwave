using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetGeoZoneForEditQuery
    {
        public Guid GeoZoneId { get; set; }
    }
}
