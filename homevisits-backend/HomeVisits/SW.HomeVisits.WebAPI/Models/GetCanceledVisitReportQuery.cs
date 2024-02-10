using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetCanceledVisitReportQuery : IGetCanceledVisitReportQuery
    {
        public DateTime VisitDateFrom { get; set ; }
        public DateTime VisitDateTo { get; set ; }
        public Guid? CountryOption { get; set; }
        public Guid? GovernorateOption { get; set; }
        public Guid? AreaOption { get; set; }
        public int? CancellationReason { get; set; }
        public Guid UserId { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }

        public CultureNames cultureName { get; set; }
    }
}
