using System;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetGeoZonesKeyValueQuery: IGetGeoZonesKeyValueQuery
    {
       public Guid? GovernateId { get; set; }
       public CultureNames CultureName { get; set; }

        public Guid? ClientId { get; set; }
    }
}
