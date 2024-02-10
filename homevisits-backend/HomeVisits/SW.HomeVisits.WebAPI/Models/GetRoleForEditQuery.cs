using System;
using SW.HomeVisits.Application.Abstract.Queries;

namespace SW.HomeVisits.WebAPI.Models
{
    public class GetRoleForEditQuery : IGetRoleForEditQuery
    {
        public Guid RoleId { get; set; }
    }
}
