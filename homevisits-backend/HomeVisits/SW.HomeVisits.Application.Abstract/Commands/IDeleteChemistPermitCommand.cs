using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteChemistPermitCommand
    {
        Guid ChemistPermitId { get; }
        Guid ClientId { get; }
    }
}
