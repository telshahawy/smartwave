using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class SearchChemistVisitsOrderQueryResponse : ISearchChemistVisitsOrderQueryResponse
    {
        public List<ChemistVisitsOrderDto> Visits { get; set; }

        public int TotalCount { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
