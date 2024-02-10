using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetAllAgeSegmentsQuery : IGetAllAgeSegmentsQuery
    {
        public Guid ClientId { get; set; }
    }
}
