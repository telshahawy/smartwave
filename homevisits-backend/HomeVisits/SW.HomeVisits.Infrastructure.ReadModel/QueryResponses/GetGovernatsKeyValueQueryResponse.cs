using System;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using System.Collections.Generic;
using SW.HomeVisits.Application.Abstract.Dtos;
namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetGovernatsKeyValueQueryResponse: IGetGovernatsKeyValueQueryResponse
    {
       public List<GovernatsKeyValueDto> Governats { get; set; }
    }
}
