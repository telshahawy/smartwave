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
    public class SearchAgeSegmentsQueryHandler : IQueryHandler<ISearchAgeSegmentsQuery, ISearchAgeSegmentsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchAgeSegmentsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchAgeSegmentsQueryResponse Read(ISearchAgeSegmentsQuery query)
        {
            IQueryable<AgeSegmentsView> dbQuery = _context.AgeSegmentsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId &&
                (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.Name) || x.Name == query.Name) &&
                (query.IsActive == null || x.IsActive == query.IsActive) && (query.NeedExpert == null || x.NeedExpert == query.NeedExpert)
                ).OrderBy(x=>x.Code);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }
            return new SearchAgeSegmentsQueryResponse
            {
                AgeSegments = dbQuery.Select(x => new AgeSegmentsDto
                {
                   AgeSegmentId = x.AgeSegmentId,
                   Code = x.Code,
                   Name = x.Name,
                   AgeFromDay = x.AgeFromDay,
                   AgeFromMonth = x.AgeFromMonth,
                   AgeFromYear = x.AgeFromYear,
                   AgeFromInclusive = x.AgeFromInclusive,
                   AgeToDay = x.AgeToDay,
                   AgeToMonth = x.AgeToMonth,
                   AgeToYear = x.AgeToYear,
                   AgeToInclusive = x.AgeToInclusive,
                   IsActive = x.IsActive,
                   IsDeleted = x.IsDeleted,
                   NeedExpert = x.NeedExpert
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchAgeSegmentsQueryResponse;
        }
    }
}
