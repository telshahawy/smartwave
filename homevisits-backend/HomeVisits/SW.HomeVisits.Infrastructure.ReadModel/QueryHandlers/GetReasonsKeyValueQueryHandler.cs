using System;
using Common.Logging;
using SW.Framework.Cqrs;
using System.Linq;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using System.Collections.Generic;
namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetReasonsKeyValueQueryHandler : IQueryHandler<IGetReasonsKeyValueQuery, IGetReasonsKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetReasonsKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetReasonsKeyValueQueryResponse Read(IGetReasonsKeyValueQuery query)
        {
            IQueryable<ReasonsView> dbQuery = _context.ReasonsViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(r => r.VisitTypeActionId == query.VisitTypeActionId && r.ClientId == query.ClientId && r.IsActive && !r.IsDeleted);
            }

            return new GetReasonsKeyValueQueryResponse()
            {
                ActionReasons = dbQuery.Select(r => new ActionReasonKeyValueDto
                {
                    ReasonId = r.ReasonId,
                    Name = r.ReasonName
                }).ToList()
            } as IGetReasonsKeyValueQueryResponse;
        }
    }
}
