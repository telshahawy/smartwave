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
    public class SearchGovernatsQueryHandler : IQueryHandler<ISearchGovernatsQuery, ISearchGovernatsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchGovernatsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchGovernatsQueryResponse Read(ISearchGovernatsQuery query)
        {
            IQueryable<GovernateView> dbQuery = _context.GovernateViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId &&
                (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.Name) || x.CountryNameEn == query.Name) &&
                (query.IsActive == null || x.IsActive == query.IsActive) && (query.CountryId == null || x.CountryId == query.CountryId)
                ).OrderBy(x => x.Code);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }
            return new SearchGovernatsQueryResponse
            {
                Governats = dbQuery.Select(x => new GovernatsDto
                {
                    GovernateId = x.GovernateId,
                    Code = x.Code,
                    GovernateName = x.GoverNameEn,
                    CustomerServiceEmail = x.CustomerServiceEmail,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CountryName = x.CountryNameEn,
                    CountryId = x.CountryId
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchGovernatsQueryResponse;
        }
    }
}
