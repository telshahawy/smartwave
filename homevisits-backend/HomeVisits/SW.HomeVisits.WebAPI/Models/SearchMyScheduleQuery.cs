using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchMyScheduleQuery : ISearchMyScheduleQuery
    {
        public Guid ChemistId { get; set; }
        public string Order { get; set; }
        public string VisitDate { get; set; }
        public CultureNames CultureName { get; set; }
        public Guid ClientId { get; set; }
        public int Status { get; set; }
    }
}
