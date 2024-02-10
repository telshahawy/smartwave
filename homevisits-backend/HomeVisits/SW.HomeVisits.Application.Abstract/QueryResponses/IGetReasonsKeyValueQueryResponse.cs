using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Collections.Generic;
namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetReasonsKeyValueQueryResponse
    {
        List<ActionReasonKeyValueDto> ActionReasons { get; }
    }
}
