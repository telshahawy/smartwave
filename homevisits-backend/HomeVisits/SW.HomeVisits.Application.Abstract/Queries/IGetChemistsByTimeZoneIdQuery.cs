using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistsByTimeZoneIdQuery
    {
        public Guid ClientId { get; set; }
        public Guid TimeZoneGeoZoneId { get; set; }
        DateTime date { get; set; }
    }
}
