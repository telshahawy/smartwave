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
    public class GetGeoZonesKeyValueQueryHandler : IQueryHandler<IGetGeoZonesKeyValueQuery, IGetGeoZonesKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetGeoZonesKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetGeoZonesKeyValueQueryResponse Read(IGetGeoZonesKeyValueQuery query)
        {
            IQueryable<GeoZoneView> dbQuery = _context.GeoZoneView;
            if (query != null && query.CultureName != null)
            {
                dbQuery = dbQuery.Where(x=>x.ClientId == query.ClientId && x.IsActive && !x.IsDeleted && x.GovernatIsActive == true && x.CountryIsActive == true && x.GovernatIsDeleted  != true && x.CountryIsDeleted != true).AsQueryable();
                if (query.GovernateId != null && query.GovernateId != null)
                {
                    dbQuery = dbQuery.Where(x => x.governateId == query.GovernateId);
                }
            }

            return new GetGeoZonesKeyValueQueryResponse()
            {
                GeoZones = dbQuery.Select(x => new GeoZoneKeyValueDto
                {
                    GeoZoneId = x.GeoZoneId,
                    Name = query.CultureName == CultureNames.ar ? x.NameAr : x.NameEn
                }).ToList()
            } as IGetGeoZonesKeyValueQueryResponse;
        }
    }
}
