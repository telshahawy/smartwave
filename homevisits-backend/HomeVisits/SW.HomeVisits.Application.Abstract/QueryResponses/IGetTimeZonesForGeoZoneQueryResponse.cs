using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetTimeZonesForGeoZoneQueryResponse
    {
       public List< TimeZonesForGeoZoneDto> TimeZonesForGeoZone { get; set; }
    }
}
