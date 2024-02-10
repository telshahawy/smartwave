using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteChemistScheduleCommand
    {
        Guid ChemistScheduleId { get; }
        Guid ClientId { get; }
    }
}
