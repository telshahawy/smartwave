using System;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetAvailableVisitsInAreaQuery : IGetAvailableVisitsInAreaQuery
    {
        public Guid GeoZoneId { get; set; }
        public DateTime date { get; set; }
        public Guid ClientId { get; set; }
        public CultureNames CultureName { get; set; }
    }
}
