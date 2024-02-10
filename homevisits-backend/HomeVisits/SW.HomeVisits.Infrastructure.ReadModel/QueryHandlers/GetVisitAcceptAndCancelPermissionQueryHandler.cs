using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetVisitAcceptAndCancelPermissionQueryHandler : IQueryHandler<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptAndCancelPermissionQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetVisitAcceptAndCancelPermissionQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetVisitAcceptAndCancelPermissionQueryResponse Read(IGetVisitAcceptCancelPermissionQuery query)
        {
            IQueryable<SystemParametersView> dbQuery = _context.SystemParametersViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            var systemParameters = dbQuery.SingleOrDefault(x => x.ClientId == query.ClientId);
            if (systemParameters == null)
            {
                return null;
            }
            var acceptedBy = dbQuery.Select(x => x.VisitApprovalBy).FirstOrDefault();
            var cancelledBy = dbQuery.Select(x => x.VisitCancelBy).FirstOrDefault();
            var acceptedByChemist = false;
            var acceptedByCallCenter = false;
            var rejectedByChemist = false;
            var rejectedByCallCenter = false;
            if (acceptedBy.ToLower().ToString()=="chemist"&& acceptedBy.ToLower().ToString() != "both")
            {
                acceptedByChemist = true;
                acceptedByCallCenter = false;
            }
            if (acceptedBy.ToLower().ToString() == "callcenter" && acceptedBy.ToLower().ToString() != "both")
            {
                acceptedByCallCenter = true;
                acceptedByChemist = false;
            }if (cancelledBy.ToLower().ToString()=="chemist"&& cancelledBy.ToLower().ToString() != "both")
            {
                rejectedByChemist = true;
                rejectedByCallCenter = false;
            }
            if (cancelledBy.ToLower().ToString() == "callcenter" && cancelledBy.ToLower().ToString() != "both")
            {
                rejectedByChemist = false;
                rejectedByCallCenter = true;
            }
            if (acceptedBy.ToLower().ToString() =="both")
            {
                acceptedByChemist = true;
                acceptedByCallCenter = true;
            } 
            if (cancelledBy.ToLower().ToString() =="both")
            {
                rejectedByChemist = true;
                rejectedByCallCenter = true;
            }
           
            return new GetVisitAcceptAndCancelPermissionQueryResponse
            {
               IsApprovedByChemist=acceptedByChemist,
               IsCancelledByChemist=rejectedByChemist,
               IsApprovedByCallCenter=acceptedByCallCenter,
               IsCancelledByCallCenter=rejectedByCallCenter

            } as IGetVisitAcceptAndCancelPermissionQueryResponse;
        }
    }
}
