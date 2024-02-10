using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistPermitForEditQuery
    {
        Guid ChemistPermitId { get; }
        Guid ClientId { get; }
    }
}
