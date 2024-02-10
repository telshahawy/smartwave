using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetCountryForEditQuery : IGetCountryForEditQuery
    {
        public Guid CountryId { get; set; }
    }
}
