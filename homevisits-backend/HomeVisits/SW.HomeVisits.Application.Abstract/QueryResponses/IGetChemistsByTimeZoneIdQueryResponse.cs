using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetChemistsByTimeZoneIdQueryResponse
    {
        List<AvailableChemistsDto> AvailableChemists { get; }

    }
}
