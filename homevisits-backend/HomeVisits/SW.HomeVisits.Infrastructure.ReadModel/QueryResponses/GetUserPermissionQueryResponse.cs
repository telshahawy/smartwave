using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetUserPermissionQueryResponse : IGetUserPermissionQueryResponse
    {
        public List<SystemPagePermissionDto> SystemPagePermissionDtos { get; set; }
        //public UserPermissionDto UserPermission { get; set; }
    }
}
