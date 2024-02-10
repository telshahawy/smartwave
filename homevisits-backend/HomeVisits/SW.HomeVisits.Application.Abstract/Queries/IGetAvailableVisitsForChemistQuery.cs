using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetAvailableVisitsForChemistQuery
    {
        Guid ChemistId { get; }
        Guid? GeoZoneId { get; }
        DateTime Date { get; }
        Guid ClientId { get; }
        CultureNames CultureName { get; }
    }
}
