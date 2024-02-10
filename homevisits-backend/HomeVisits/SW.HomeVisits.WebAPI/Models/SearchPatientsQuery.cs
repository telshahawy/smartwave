using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchPatientsQuery : ISearchPatientsQuery
    {
        public string PhoneNumber { get; set; }
        public CultureNames CultureName { get; set; }
        public Guid ClientId { get; set; }
    }
}
