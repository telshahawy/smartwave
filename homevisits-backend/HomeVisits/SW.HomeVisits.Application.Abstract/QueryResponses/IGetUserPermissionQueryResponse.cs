using System;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetUserPermissionQueryResponse
    {
        public List<SystemPagePermissionDto> SystemPagePermissionDtos { get; set; }
        //public UserPermissionDto UserPermission { get; set; }
    }
}
