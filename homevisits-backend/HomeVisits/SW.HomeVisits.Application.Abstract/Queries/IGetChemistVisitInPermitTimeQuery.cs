using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistVisitInPermitTimeQuery
    {
        Guid ChemistId { get; }
        Guid ClientId { get; }
        DateTime PermitDate { get; }
        TimeSpan PermitStartTime { get; set; }
        TimeSpan PermitEndTime { get; set; }
    }
}
