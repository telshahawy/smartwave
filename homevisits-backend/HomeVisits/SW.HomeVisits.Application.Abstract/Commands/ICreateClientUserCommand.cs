using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateClientUserCommand
    {
        Guid UserId { get; }
        string Name { get;}
        string PhoneNumber { get; }
        Guid RoleId { get; }
        List<Guid> GeoZones { get; }
        bool IsActive { get; }
        string UserName { get; }
        string Password { get; }
        Guid ClientId { get; }
        Guid CreateBy { get; }
        List<int> Permissions { get; set; }
    }
}
