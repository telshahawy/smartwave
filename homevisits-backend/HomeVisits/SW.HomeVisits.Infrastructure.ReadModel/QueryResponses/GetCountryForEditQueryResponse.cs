using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetCountryForEditQueryResponse : IGetCountryForEditQueryResponse
    {
        public CountriesDto Country { get; set; }
    }
}
