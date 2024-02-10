using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistAssignedGeoZonesKeyValueQuery : IGetChemistAssignedGeoZoneKeyValueQuery
    {
        public Guid ChemistId { get; set; }

        public Guid ClientId { get; set; }
    }
}
