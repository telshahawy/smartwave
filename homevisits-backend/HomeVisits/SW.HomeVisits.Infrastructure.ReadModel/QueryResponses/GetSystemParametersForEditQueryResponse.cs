using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetSystemParametersForEditQueryResponse : IGetSystemParametersForEditQueryResponse
    {
        public SystemParametersDto SystemParameters { get; set; }
    }
}
