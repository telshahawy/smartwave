using System;
using SW.HomeVisits.Application.Abstract.Queries;
namespace SW.HomeVisits.WebAPI.Models
{
    public class GetCityQuery:IGetCityQuery
    {
        public Guid CityId { get; set; }
    }
}
