using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetReasonForEditQueryHandler : IQueryHandler<IGetReasonForEditQuery, IGetReasonForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetReasonForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetReasonForEditQueryResponse Read(IGetReasonForEditQuery query)
        {
            IQueryable<ReasonsView> dbQuery = _context.ReasonsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var reason = dbQuery.SingleOrDefault(x => x.ReasonId == query.ReasonId);
            if (reason == null)
            {
                throw new Exception("Reason not found");
            }
            return new GetReasonForEditQueryResponse
            {
                Reason = new ReasonsDto
                {
                    ReasonId = reason.ReasonId,
                    ReasonName = reason.ReasonName,
                    IsActive = reason.IsActive,
                    ReasonActionId = reason.ReasonActionId,
                    ReasonActionName = reason.ReasonActionName
                }
            
            } as IGetReasonForEditQueryResponse;
        }
    }
}

