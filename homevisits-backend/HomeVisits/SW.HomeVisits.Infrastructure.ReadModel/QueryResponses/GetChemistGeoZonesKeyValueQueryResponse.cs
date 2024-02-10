using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetChemistGeoZonesKeyValueQueryResponse : IGetChemistGeoZonesKeyValueQueryResponse
    {
        public List<GeoZoneKeyValueDto> GeoZones { get; set; }
    }
}
