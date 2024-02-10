using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetTimeZoneFramesQuery : IGetTimeZoneFramesQuery
    {
        public Guid? ClientId { get; set; }
        public Guid? GeoZoneId { get; set; }
        public Guid? TimeZoneFrameId { get; set; }
        public CultureNames CultureName { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
