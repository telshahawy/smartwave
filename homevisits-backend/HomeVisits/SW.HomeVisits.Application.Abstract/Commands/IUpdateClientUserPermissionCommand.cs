using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IUpdateClientUserPermissionCommand
    {
        Guid UserId { get; set; }
        Guid ClientId { get; set; }
        List<int> Permissions { get; set; }
    }
}
