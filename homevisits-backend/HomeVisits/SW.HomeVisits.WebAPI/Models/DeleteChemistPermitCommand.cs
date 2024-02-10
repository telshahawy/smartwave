using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteChemistPermitCommand : IDeleteChemistPermitCommand
    {
        public Guid ChemistPermitId { get; set; }

        public Guid ClientId { get; set; }
    }
}
