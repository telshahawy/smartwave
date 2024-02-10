using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetChemistPermitForEditQuery : IGetChemistPermitForEditQuery
    {
        public Guid ChemistPermitId { get; set; }

        public Guid ClientId { get; set; }
    }
}
