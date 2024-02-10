using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class SearchChemistPermitsModel
    {
        public Guid ChemistId { get; set; }
        public DateTime? PermitDate { get; set; }
    }
}
