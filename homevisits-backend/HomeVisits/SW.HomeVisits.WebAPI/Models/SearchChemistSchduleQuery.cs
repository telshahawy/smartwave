using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchChemistSchduleQuery : ISearchChemistScheduleQuery
    {
        public Guid ChemistId { get; set; }
        public Guid ClientId {get;set;}

        public DateTime? StartDate {get;set;}

        public DateTime? EndDate {get;set;}

        public Guid? AssignedGeoZoneId {get;set;}
    }
}
