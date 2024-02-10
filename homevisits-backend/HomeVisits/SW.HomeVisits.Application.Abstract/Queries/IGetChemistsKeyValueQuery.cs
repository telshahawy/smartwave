using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistsKeyValueQuery
    {
        Guid? GeoZoneId { get; set; }
        CultureNames CultureName { get; }
        Guid ClientId { get; set; }
    }
}
