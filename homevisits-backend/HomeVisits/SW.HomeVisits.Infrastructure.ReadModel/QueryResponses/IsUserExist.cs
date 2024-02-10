using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class IsUserExist : IIsUserExist
    {
        public bool IsExist { get; set; }
    }
}
