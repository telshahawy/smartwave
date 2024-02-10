using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchChemistPermitsQuery : ISearchChemistPermitsQuery
    {
        public Guid ChemistId { get; set; }

        public Guid ClientId { get; set; }

        public DateTime? PermitDate { get; set; }
    }
}
