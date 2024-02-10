using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistScheduleForEditQuery
    {
        Guid ChemistScheduleId { get; }
        Guid ClientId { get; }
    }
}
