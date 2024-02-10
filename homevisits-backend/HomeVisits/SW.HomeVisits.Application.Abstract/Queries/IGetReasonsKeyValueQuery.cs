using System;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetReasonsKeyValueQuery
    {
        int VisitTypeActionId { get; }
        Guid ClientId { get; }
    }
}
