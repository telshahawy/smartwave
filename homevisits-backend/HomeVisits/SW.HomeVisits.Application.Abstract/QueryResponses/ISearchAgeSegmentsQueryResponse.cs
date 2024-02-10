using System;
using System.Collections.Generic;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface ISearchAgeSegmentsQueryResponse : IPaggingResponse
    {
        public List<AgeSegmentsDto> AgeSegments { get; set; }
    }
}
