using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetRejectedVisitReportQueryResponse : IGetRejectedVisitReportQueryResponse
    {
        public string DateFrom {get;set;}
        public string DateTo {get;set;}
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string Chemist { get; set; }
        public string Delayed { get; set; }
        public string Area { get; set; }
        public string Reason { get; set; }
        public int TotalVisitsNo { get; set; }
        public int CancelledVisitsNo { get; set; }
        public int ReassignedVisitsNo { get; set; }
        public List<RejectedVisitReportDto> RejectedVisitReports { get; set; }
        public string PrintedBy { get; set; }
        public string PrintedDate{ get; set; }

        public int TotalCount { get; set; }

        public int? PageSize { get; set; }

        public int? CurrentPageIndex { get; set; }
    }                           
}
