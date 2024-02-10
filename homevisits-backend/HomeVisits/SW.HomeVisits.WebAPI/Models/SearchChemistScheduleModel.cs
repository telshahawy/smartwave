using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchChemistScheduleModel
    {
        public Guid ChemistId { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? AssignedGeoZoneId { get; set; }
    }
}
