using SW.HomeVisits.Application.Abstract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.HomeVisits.WebAPI.Models
{
    public class UpdateClientUserPermissionCommand : IUpdateClientUserPermissionCommand
    {
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public List<int> Permissions { get; set; }
    }
}
