using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchAgeSegmentsQuery : IPaggingQuery
    {
        int? Code { get; }
        string Name { get; }
        bool? IsActive { get; }
        bool? NeedExpert { get; }
        Guid ClientId { get; }

    }
}
