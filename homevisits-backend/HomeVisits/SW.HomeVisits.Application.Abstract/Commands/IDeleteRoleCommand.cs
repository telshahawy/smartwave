using System;
namespace SW.HomeVisits.Application.Abstract.Commands
{
    public interface IDeleteRoleCommand
    {
        Guid RoleId { get; set; }
    }
}
