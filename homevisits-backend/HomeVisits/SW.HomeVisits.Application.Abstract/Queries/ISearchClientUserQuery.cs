using System;
using SW.Framework.Utilities;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchClientUserQuery : IPaggingQuery
    {
        int? Code { get; }
        string Name { get; }
        string PhoneNumber { get; }
        bool? IsActive { get; }
        Guid ClientId { get; }
    }
}
