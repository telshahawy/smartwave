using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetActiveChemistHomePageQueryResponse
    {
        public List<string> ActiveChemistNames { get; set; }

    }
}
