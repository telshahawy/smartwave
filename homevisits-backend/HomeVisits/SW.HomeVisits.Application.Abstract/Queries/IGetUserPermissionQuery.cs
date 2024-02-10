using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetUserPermissionQuery
    {
        Guid UserId { get; set; }
        Guid ClientId { get; set; }
    }
}
