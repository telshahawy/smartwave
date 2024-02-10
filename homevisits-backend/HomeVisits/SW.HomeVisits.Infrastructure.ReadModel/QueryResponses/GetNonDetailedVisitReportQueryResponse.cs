using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetNonDetailedVisitReportQueryResponse : IGetNonDetailedVisitReportQueryResponse
    {
        public string VisitDateFrom { get; set; }
        public string VisitDateTo { get; set; }
        public string DelayedOption { get; set; }
        public string CountryOption { get; set; }
        public string GovernorateOption { get; set; }
        public string ChemistOption { get; set; }
        public string AreaOption { get; set; }
        public int TotalVisitsNo { get; set; }
        public List<NonDetailedVisitReportDto> NonDetailedVisitReports { get; set; }
        public string PrintedBy { get; set; }
        public string PrintedDate { get; set; }

        public int TotalCount { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }
}
