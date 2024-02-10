using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetActiveChemistHomePageQueryResponse : IGetActiveChemistHomePageQueryResponse
    {
        public List<string> ActiveChemistNames { get ; set; }
    }
}
