using System;
using System.Collections.Generic;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface ICreateRoleCommand
    {
        Guid RoleId { get; set; }
        Guid ClientId { get; set; }
        string NameAr { get; set; }
        string NameEn { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        Guid CreatedBy { get; set; }
        int DefaultPageId { get; set; }
        List<int> Permissions { get; set; }
        List<Guid> GeoZones { get; set; }
    }
}
             