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
    public class SearchReasonsQueryHandler : IQueryHandler<ISearchReasonsQuery, ISearchReasonsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchReasonsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchReasonsQueryResponse Read(ISearchReasonsQuery query)
        {
            IQueryable<ReasonsView> dbQuery = _context.ReasonsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId && x.VisitTypeActionId == query.VisitTypeActionId &&
                (string.IsNullOrWhiteSpace(query.ReasonName) || x.ReasonName == query.ReasonName) && (query.ReasonId == null || x.ReasonId == query.ReasonId) &&
                (query.IsActive  == null || x.IsActive == query.IsActive)
                );

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }
            return new SearchReasonsQueryResponse
            {
                Reasons = dbQuery.Select(x => new ReasonsDto
                {
                    ReasonId = x.ReasonId,
                    ReasonName = x.ReasonName,
                    IsActive = x.IsActive,
                    ReasonActionId = x.ReasonActionId,
                    ReasonActionName = x.ReasonActionName
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchReasonsQueryResponse;
        }
    }
}
