using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetCanceledVisitReportQueryResponse : IPaggingResponse
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Country { get; set; }
        public string Governorate { get; set; }
        public string Area { get; set; }
        public string reason { get; set; }
        public List<CanceledVisitReportDto> canceledVisitReports { get; set; }
        public string PrintedBy { get; set; }
        public string PrintedDate { get; set; }
    }
}
