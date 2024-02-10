using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetVisitDatesRegardingSysParamQueryResponse
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}
