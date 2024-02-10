using System;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Enum;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface ISearchGovernatsQuery : IPaggingQuery
    {
        int? Code { get; }
        string Name { get; }
        bool? IsActive { get; }
        Guid? CountryId { get; }
        Guid ClientId { get; }
    }
}
