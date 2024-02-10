using System;
namespace SW.HomeVisits.Application.Abstract.Queries
{
    public interface IGetRoleForEditQuery
    {
        public Guid RoleId { get; set; }
    }
}
