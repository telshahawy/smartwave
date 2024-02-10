using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class SearchPatientScheduleQueryResponse : ISearchPatientScheduleQueryResponse
    {
        public List<VisitsDto> TodaysVisits { get ; set ; }
        public List<VisitsDto> OldVisits { get ; set ; }

        public int TotalCount { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
