using System;
using System.Collections.Generic;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface ISearchVisitsQueryResponse : IPaggingResponse
    {
        List<VisitsDto> Visits { get; set; }
    }
}
