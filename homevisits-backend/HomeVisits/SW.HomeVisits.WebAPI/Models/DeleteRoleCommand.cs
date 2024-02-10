using System;
using SW.HomeVisits.Application.Abstract.Commands;

namespace SW.HomeVisits.WebAPI.Models
{
    public class DeleteRoleCommand:IDeleteRoleCommand
    {
        

        public Guid RoleId { get; set; }
    }
}
