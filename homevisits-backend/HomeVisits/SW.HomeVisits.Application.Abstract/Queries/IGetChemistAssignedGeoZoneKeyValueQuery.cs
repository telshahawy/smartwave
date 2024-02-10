using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistAssignedGeoZoneKeyValueQuery
    {
        Guid ChemistId { get; }
        Guid ClientId { get; }
    }
}
