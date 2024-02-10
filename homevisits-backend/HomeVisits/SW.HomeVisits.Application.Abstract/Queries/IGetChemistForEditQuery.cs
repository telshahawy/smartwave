using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistForEditQuery
    {
        Guid ChemistId { get; set; }
    }
}
