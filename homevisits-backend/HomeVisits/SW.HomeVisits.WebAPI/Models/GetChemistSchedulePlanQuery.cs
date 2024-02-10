using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistSchedulePlanQuery : IGetChemistSchedulePlanQuery
    {
        public Guid ClientId { get; set; }
        public Guid ChemistId { get; set; }
        public DateTime date { get; set; }
        public bool DayFilter { get; set; }
    }
}
