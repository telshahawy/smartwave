using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetVisitReportQueryResponse : IPaggingResponse
    {
        public string VisitDateFrom { get; set; }
        public string VisitDateTo { get; set; }
        public string DelayedOption { get; set; }//all,no,yes
        public string CountryOption { get; set; }
        public string GovernorateOption { get; set; }
        public string ChemistOption { get; set; }
        public string AreaOption { get; set; }
        public int TotalVisitsNo { get; set; }
        public List<VisitReportDto> VisitReports { get; set; }
        public string PrintedBy { get; set; }
        public string PrintedDate { get; set; }


    }
}
