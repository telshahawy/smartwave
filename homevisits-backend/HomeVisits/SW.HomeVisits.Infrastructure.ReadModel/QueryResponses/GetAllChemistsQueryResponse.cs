using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Collections.Generic;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetAllChemistsQueryResponse : IGetAllChemistsQueryResponse
    {
        public List<ChemistDto> Chemists { get; set; }
    }
}
