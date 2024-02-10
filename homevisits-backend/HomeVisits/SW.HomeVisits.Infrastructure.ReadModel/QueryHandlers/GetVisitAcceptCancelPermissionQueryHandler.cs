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
    public class GetVisitAcceptCancelPermissionQueryHandler : IQueryHandler<IGetVisitAcceptCancelPermissionQuery, IGetVisitAcceptCancelPermissionQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetVisitAcceptCancelPermissionQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetVisitAcceptCancelPermissionQueryResponse Read(IGetVisitAcceptCancelPermissionQuery query)
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
            var value = false;
           
            if (query.IsAcceptedBy?.ToLower().ToString() == "chemist")
            {
                if (acceptedBy.ToLower().ToString() == "chemist" || acceptedBy.ToLower().ToString() == "both")
                {
                    value = true;

                }
                else
                {
                    value = false;
                }
            }
             else if(query.IsAcceptedBy?.ToLower().ToString() == "callcenter")
            {
                if (acceptedBy.ToLower().ToString() == "callcenter" || acceptedBy.ToLower().ToString() == "both")
                {
                    value = true;

                }
                else
                {
                    value = false;
                }
            }

            if (query.IsCancelledBy?.ToLower().ToString() == "chemist")
            {
                if (cancelledBy.ToLower().ToString() == "chemist" || cancelledBy.ToLower().ToString() == "both")
                {
                    value = true;

                }
                else
                {
                    value = false;
                }
            }
            else if (query.IsCancelledBy?.ToLower().ToString() == "callcenter")
            {
                if (cancelledBy.ToLower().ToString() == "callcenter" || cancelledBy.ToLower().ToString() == "both")
                {
                    value = true;

                }
                else
                {
                    value = false;
                }
            }

            return new GetVisitAcceptCancelPermissionQueryResponse
            {
               Value=value

            } as IGetVisitAcceptCancelPermissionQueryResponse;

        }
    }
}
