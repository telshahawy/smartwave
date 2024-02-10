﻿using System;
using System.Collections.Generic;
using SW.Framework.Utilities;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface ISearchRolesQueryResponse:IPaggingResponse
    {
        public List<SearchRoleDto> Roles { get; set; }
    }
}
