using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchReasonsModel : IPaggingQuery
    {
        public int? ReasonId { get; set; }
        public string ReasonName { get; set; }

        public bool? IsActive { get; set; }
        public int VisitTypeActionId { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
