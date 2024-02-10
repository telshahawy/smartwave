using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class SearchAgeSegmentsQueryResponse : ISearchAgeSegmentsQueryResponse
    {
        public List<AgeSegmentsDto> AgeSegments { get; set; }
        public int TotalCount { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
