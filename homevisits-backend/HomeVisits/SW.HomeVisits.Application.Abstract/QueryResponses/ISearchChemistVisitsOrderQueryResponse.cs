using System;
using System.Collections.Generic;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface ISearchChemistVisitsOrderQueryResponse : IPaggingResponse
    {
        List<ChemistVisitsOrderDto> Visits { get; set; }
    }
}
