using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetClientUserForEditQuery
    {
        Guid UserId { get; }
        Guid ClientId { get; }
    }
}
