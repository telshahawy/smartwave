using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistsKeyValueQuery : IGetChemistsKeyValueQuery
    {
        public Guid? GeoZoneId { get; set; }

        public CultureNames CultureName { get; set; }

        public Guid ClientId { get; set; }
    }
}
