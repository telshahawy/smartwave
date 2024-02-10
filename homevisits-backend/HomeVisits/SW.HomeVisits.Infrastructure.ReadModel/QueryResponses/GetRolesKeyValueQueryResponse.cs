using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetRolesKeyValueQueryResponse : IGetRolesKeyValueQueryResponse
    {
        public List<RoleKeyValueDto> Roles { get; set; }
    }
}
