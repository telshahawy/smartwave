using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchRolesQuery: IPaggingQuery
    {
        int? Code { get;}
        string Name { get; }
        bool? IsActive { get; }
        Guid ClientId { get; }
    }
}
