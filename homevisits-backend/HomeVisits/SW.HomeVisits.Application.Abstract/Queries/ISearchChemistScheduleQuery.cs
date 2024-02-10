using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchChemistScheduleQuery
    {
        Guid ChemistId { get; }
        Guid ClientId { get; }
        DateTime? StartDate { get; }
        DateTime? EndDate { get; }
        Guid? AssignedGeoZoneId { get; }
    }
}
