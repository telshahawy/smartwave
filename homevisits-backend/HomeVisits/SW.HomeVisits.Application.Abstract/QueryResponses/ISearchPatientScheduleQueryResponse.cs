using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface ISearchPatientScheduleQueryResponse : IPaggingResponse
    {
        List<VisitsDto> TodaysVisits { get; set; }
        List<VisitsDto> OldVisits { get; set; }
    }
}
