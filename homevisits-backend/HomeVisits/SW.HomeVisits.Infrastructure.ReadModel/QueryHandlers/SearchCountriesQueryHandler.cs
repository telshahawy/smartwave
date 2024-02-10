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
    public class SearchCountriesQueryHandler : IQueryHandler<ISearchCountriesQuery, ISearchCountriesQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchCountriesQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchCountriesQueryResponse Read(ISearchCountriesQuery query)
        {
            IQueryable<CountryView> dbQuery = _context.CountryViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId &&
                (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.Name) || x.CountryNameEn == query.Name) &&
                (query.IsActive == null || x.IsActive == query.IsActive)
                ).OrderBy(x => x.Code);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchCountriesQueryResponse
            {
                Countries = dbQuery.Select(x => new CountriesDto
                {
                    CountryId = x.CountryId,
                    Code = x.Code,
                    CountryName = x.CountryNameEn,
                    MobileNumberLength = x.MobileNumberLength,
                    IsActive = x.IsActive
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchCountriesQueryResponse;
        }
    }
}
