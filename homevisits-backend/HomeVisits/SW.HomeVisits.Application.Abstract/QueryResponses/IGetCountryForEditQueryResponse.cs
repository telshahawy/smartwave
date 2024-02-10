using System;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetCountryForEditQueryResponse
    {
         CountriesDto Country { get; set; }
    }
}
