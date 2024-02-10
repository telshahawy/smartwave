using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchChemistPermitsQuery
    {
        Guid ChemistId { get; }
        Guid ClientId { get; }
        DateTime? PermitDate { get; }
    }
}
