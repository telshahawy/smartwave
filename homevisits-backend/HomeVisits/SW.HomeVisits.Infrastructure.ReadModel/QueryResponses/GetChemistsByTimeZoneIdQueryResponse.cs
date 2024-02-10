using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Collections.Generic;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetChemistsByTimeZoneIdQueryResponse : IGetChemistsByTimeZoneIdQueryResponse
    {
        public List<AvailableChemistsDto> AvailableChemists { get; set; }
    }
}
