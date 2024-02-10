using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetAllChemistHomePageQueryResponse: IGetAllChemistHomePageQueryResponse
    {
        public int No { get; set; }
    }
}
