using SW.HomeVisits.Application.Abstract.Queries;
using System;

namespace SW.HomeVisits.Application.Models
{
    public class GetChemistsByTimeZoneIdQuery : IGetChemistsByTimeZoneIdQuery
    {
        public Guid ClientId { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        public DateTime date { get; set; }
    }
}
