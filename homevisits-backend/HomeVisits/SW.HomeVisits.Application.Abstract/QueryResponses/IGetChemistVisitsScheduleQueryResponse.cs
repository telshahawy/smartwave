using System;
using System.Collections.Generic;
using System.Text;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetChemistVisitsScheduleQueryResponse
    {
        public List<ChemistVisitsScheduleDto> ChemistVisitsScheduleDtos { get; set; }

    }
}
