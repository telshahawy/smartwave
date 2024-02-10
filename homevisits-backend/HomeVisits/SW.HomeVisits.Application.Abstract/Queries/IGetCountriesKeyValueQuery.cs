using System;
using SW.HomeVisits.Application.Abstract.Enum;

namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetCountriesKeyValueQuery
    {
        CultureNames CultureName { get; }
        Guid? ClientId { get; }
    }
}
