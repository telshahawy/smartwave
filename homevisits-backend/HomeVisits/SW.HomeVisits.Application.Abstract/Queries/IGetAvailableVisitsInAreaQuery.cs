using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetAvailableVisitsInAreaQuery
    {
        Guid GeoZoneId { get; set; }
        DateTime date {get;set;}
        Guid ClientId { get; set; }
        CultureNames CultureName { get; }
    }
}
