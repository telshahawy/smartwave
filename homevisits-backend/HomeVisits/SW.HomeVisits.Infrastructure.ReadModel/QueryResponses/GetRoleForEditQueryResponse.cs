using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetRoleForEditQueryResponse : IGetRoleForEditQueryResponse
    {
        public RoleDto Role { get; set; }
    }
}
