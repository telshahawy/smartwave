using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchReasonsQuery : ISearchReasonsQuery
    {
        public string ReasonName { get; set; }

        public bool? IsActive { get; set; }

        public int VisitTypeActionId { get; set; }

        public Guid ClientId { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }

        public int? ReasonId { get; set; }
    }
}
