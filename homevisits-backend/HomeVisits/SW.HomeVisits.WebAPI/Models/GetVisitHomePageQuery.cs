using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetVisitHomePageQuery : IGetVisitHomePageQuery
    {
        public Guid GeoZoneId { get ; set; }

        public int? PageSize { get; set; }
        public CultureNames cultureName { get; set; }
        public int? CurrentPageIndex { get; set; }
    }
}
