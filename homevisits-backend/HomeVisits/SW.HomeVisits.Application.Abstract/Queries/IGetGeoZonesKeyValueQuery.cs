using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetGeoZonesKeyValueQuery
    {
        Guid? GovernateId { get; }
        CultureNames CultureName { get; }
        Guid? ClientId { get; }
    }
}
