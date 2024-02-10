using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetCityQueryResponse:IGetCityQueryResponse
    {
        public CityDto city { get; set; }
    }
}
