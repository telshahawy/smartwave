using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetCanceledVisitReportQuery: IPaggingQuery
    {
        public DateTime VisitDateFrom { get; set; }
        public DateTime VisitDateTo { get; set; }
        CultureNames cultureName { get; }
        public Guid? CountryOption { get; set; }
        public Guid? GovernorateOption { get; set; }
      
        public Guid? AreaOption { get; set; }
        public int? CancellationReason { get; set; }
    
        public Guid UserId { get; set; }

    }
}
