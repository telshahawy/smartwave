using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetTimeZoneFramesQuery
    {
        public Guid? GeoZoneId { get; set; }
        public Guid? TimeZoneFrameId { get; set; }
        public CultureNames CultureName { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
