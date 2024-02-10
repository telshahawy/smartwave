using SW.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetVisitReportQuery: IPaggingQuery
    {
        public DateTime VisitDateFrom { get; set; }
        public DateTime VisitDateTo { get; set; }
        public string DelayedOption { get; set; }//all,no,yes
        public Guid? CountryOption { get; set; }
        public Guid? GovernorateOption { get; set; }
        public Guid? ChemistOption { get; set; }
        public Guid? AreaOption { get; set; }
        public bool? ShowDetails { get; set; }
        public Guid UserId { get; set; }

    }
}
