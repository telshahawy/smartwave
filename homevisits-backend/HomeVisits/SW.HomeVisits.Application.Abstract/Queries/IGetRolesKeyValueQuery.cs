using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetRolesKeyValueQuery
    {
        Guid ClientId { get; }
    }
}
