using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Collections.Generic;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetChemistSchedulePlanQueryResponse : IGetChemistSchedulePlanQueryResponse
    {
        public List<ChemistSchedulePlanDto> ChemistSchedulePlans { get; set; }
    }
}
