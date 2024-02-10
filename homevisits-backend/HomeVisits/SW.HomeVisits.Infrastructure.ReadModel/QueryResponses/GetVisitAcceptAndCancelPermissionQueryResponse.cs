using SW.HomeVisits.Application.Abstract.QueryResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryResponses
{
    public class GetVisitAcceptAndCancelPermissionQueryResponse : IGetVisitAcceptAndCancelPermissionQueryResponse
    {
        public bool IsCancelledByChemist { get ; set ; }
        public bool IsCancelledByCallCenter { get; set; }
        public bool IsApprovedByChemist { get; set ; }
        public bool IsApprovedByCallCenter { get ; set ; }
    }
}
