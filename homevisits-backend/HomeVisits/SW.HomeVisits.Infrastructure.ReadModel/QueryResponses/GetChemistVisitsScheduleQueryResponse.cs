using System;
using System.Collections.Generic;
using System.Text;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetChemistVisitsScheduleQueryResponse : IGetChemistVisitsScheduleQueryResponse
    {
        public List<ChemistVisitsScheduleDto> ChemistVisitsScheduleDtos { get; set; }
    }
}
