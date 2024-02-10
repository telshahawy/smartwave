using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDuplicateChemistScheduleCommand
    {
        Guid ChemistScheduleId { get; }
        Guid NewChemistScheduleId { get; }
        Guid ClientId { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        Guid CreatedBy { get; }
        DateTime CreatedAt { get; }
    }
}
