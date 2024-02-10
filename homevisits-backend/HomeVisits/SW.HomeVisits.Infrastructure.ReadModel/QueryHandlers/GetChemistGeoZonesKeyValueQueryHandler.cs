using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetChemistGeoZonesKeyValueQueryHandler : IQueryHandler<IGetChemistGeoZonesKeyValueQuery, IGetChemistGeoZonesKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistGeoZonesKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistGeoZonesKeyValueQueryResponse Read(IGetChemistGeoZonesKeyValueQuery query)
        {
            IQueryable<ChemistAssignedGeoZonesView> dbQuery = _context.ChemistAssignedGeoZonesViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x=> x.IsActive == true && x.IsDeleted != true).AsQueryable();
                if (query.ChemistId != null)
                {
                    dbQuery = dbQuery.Where(x => x.ChemistId == query.ChemistId);
                }
            }

            return new GetChemistGeoZonesKeyValueQueryResponse()
            {
                GeoZones = dbQuery.Select(x => new GeoZoneKeyValueDto
                {
                    GeoZoneId = x.GeoZoneId,
                    Name = query.CultureName == CultureNames.ar ? x.NameAr : x.NameEn
                }).ToList()
            } as IGetChemistGeoZonesKeyValueQueryResponse;
        }
    }
}
