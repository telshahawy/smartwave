using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchReasonsQuery : IPaggingQuery
    {
        int? ReasonId { get; }
        string ReasonName { get; }
        bool? IsActive { get; }
        int VisitTypeActionId { get; }
        Guid ClientId { get; }

    }
}
