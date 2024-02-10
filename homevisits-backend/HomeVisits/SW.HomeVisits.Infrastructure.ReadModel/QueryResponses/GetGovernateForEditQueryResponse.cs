using System;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetGovernateForEditQueryResponse : IGetGovernateForEditQueryResponse
    {
        public GovernatsDto Governate { get; set; }
    }
}
