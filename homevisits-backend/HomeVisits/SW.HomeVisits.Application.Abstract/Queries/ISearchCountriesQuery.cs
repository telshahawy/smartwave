using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchCountriesQuery : IPaggingQuery
    {
        int? Code { get; }
        string Name { get; }
        bool? IsActive { get; }
        Guid ClientId { get; }

    }
}
