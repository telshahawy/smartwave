using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistGeoZonesKeyValueQuery : IGetChemistGeoZonesKeyValueQuery
    {
        public Guid ChemistId { get; set; }

        public CultureNames CultureName { get; set; }
    }
}
