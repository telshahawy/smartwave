using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetVisitDatesRegardingSysParamQueryResponse : IGetVisitDatesRegardingSysParamQueryResponse
    {
        public DateTime StartDate{get;set;}
        public DateTime EndDate { get; set; }
    }
}
