using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistTrackingLogQuery : IGetChemistTrackingLogQuery
    {
        public Guid ChemistId { get; set; }
    }
}
