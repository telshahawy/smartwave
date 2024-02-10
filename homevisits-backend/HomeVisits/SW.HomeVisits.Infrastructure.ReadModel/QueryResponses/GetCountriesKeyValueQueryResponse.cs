using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetCountriesKeyValueQueryResponse: IGetCountriesKeyValueQueryResponse
    {
        public List<CountryKeyValueDto> Countries { get; set; }
    }
}
