using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchGeoZonesQuery : IPaggingQuery
    {
        int? Code { get; }
        string Name { get; }
        bool? IsActive { get; }
        Guid? CountryId { get; }
        Guid? GovernateId { get; }
        string MappingCode { get; }
        Guid ClientId { get; }
    }
}
