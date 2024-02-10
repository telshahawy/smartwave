using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetChemistRoutesQuery
    {
        Guid ChemistId { get; }
        float? StartLatitude { get; }
        float? StartLongitude { get; }
        CultureNames CultureName { get; }
        Guid ClientId { get; }
    }
}
