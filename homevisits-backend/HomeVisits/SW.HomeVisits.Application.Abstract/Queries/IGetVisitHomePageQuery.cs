using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetVisitHomePageQuery: IPaggingQuery
    {
        public Guid GeoZoneId { get; set; }
        public CultureNames cultureName { get; set; }
    }
}
