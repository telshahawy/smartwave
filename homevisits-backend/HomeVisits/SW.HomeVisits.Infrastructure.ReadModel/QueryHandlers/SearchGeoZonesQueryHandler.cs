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
    public class SearchGeoZonesQueryHandler : IQueryHandler<ISearchGeoZonesQuery, ISearchGeoZonesQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchGeoZonesQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchGeoZonesQueryResponse Read(ISearchGeoZonesQuery query)
        {
            IQueryable<GeoZoneView> dbQuery = _context.GeoZoneView;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId &&
                (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.Name) || x.NameEn == query.Name) &&
                (query.IsActive == null || x.IsActive == query.IsActive) && (query.CountryId == null || x.CountryId == query.CountryId)
                && (query.GovernateId == null || x.governateId == query.GovernateId) &&
                (string.IsNullOrWhiteSpace(query.MappingCode) || x.MappingCode == query.MappingCode)
                ).OrderBy(o=>o.Code);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }
            return new SearchGeoZonesQueryResponse
            {
                GeoZones = dbQuery.Select(x => new GeoZonesDto
                {
                    GeoZoneId = x.GeoZoneId,
                    GovernateId = x.governateId,
                    Code = x.Code,
                    GeoZoneName = x.NameEn,
                    GovernateName = x.GoverNameEn,
                    MappingCode = x.MappingCode,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CountryId = x.CountryId
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchGeoZonesQueryResponse;
        }
    }
}
