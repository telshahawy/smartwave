using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistGeoZonesKeyValueQuery
    {
        Guid ChemistId { get; set; }
        CultureNames CultureName { get; }
    }
}
