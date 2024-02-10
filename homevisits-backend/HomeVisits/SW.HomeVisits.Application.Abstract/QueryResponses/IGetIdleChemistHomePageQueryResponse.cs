using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.Abstract.QueryResponses
{
    public interface IGetIdleChemistHomePageQueryResponse
    {
        public List<string> IdleChemistNames { get; set; }

    }
}
