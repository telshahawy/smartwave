using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistScheduleForEditQuery : IGetChemistScheduleForEditQuery
    {
        public Guid ChemistScheduleId { get; set; }

        public Guid ClientId { get; set; }
    }
}
