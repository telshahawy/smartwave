using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetRejectedVisitReportQuery: IPaggingQuery
    {
        public DateTime VisitDateFrom { get; set; }
        public DateTime VisitDateTo { get; set; }
        public Guid? ChemistOption { get; set; }
        public Guid? CountryOption { get; set; }
        public Guid? GovernorateOption { get; set; }
        CultureNames cultureName { get; }
        public Guid? AreaOption { get; set; }
        public int? Reason { get; set; }
        public string DelayedOption { get; set; }//all,no,yes
        public Guid UserId { get; set; }
    }
}
