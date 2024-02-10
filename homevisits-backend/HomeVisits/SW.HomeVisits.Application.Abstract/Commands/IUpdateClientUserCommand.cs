using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateClientUserCommand
    {
        Guid UserId { get; }
        string Name { get; }
        string PhoneNumber { get; }
        bool IsActive { get; }
        Guid RoleId { get; }
        List<Guid> GeoZonesIds { get; set; }
        List<int> Permissions { get; set; }
    }
}
