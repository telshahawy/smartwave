using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchAgeSegmentsQuery : ISearchAgeSegmentsQuery
    {
        public int? Code { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }
        public bool? NeedExpert { get; set; }
        public Guid ClientId { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }

    }
}
