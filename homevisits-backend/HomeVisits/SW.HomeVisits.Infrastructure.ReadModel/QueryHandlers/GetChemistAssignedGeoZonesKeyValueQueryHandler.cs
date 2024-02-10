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
    public class GetChemistAssignedGeoZonesKeyValueQueryHandler : IQueryHandler<IGetChemistAssignedGeoZoneKeyValueQuery, IGetChemistAssignedGeoZoneKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistAssignedGeoZonesKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistAssignedGeoZoneKeyValueQueryResponse Read(IGetChemistAssignedGeoZoneKeyValueQuery query)
        {
            IQueryable<ChemistAssignedGeoZonesView> dbQuery = _context.ChemistAssignedGeoZonesViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x=>x.ClientId == query.ClientId && x.IsActive == true && x.IsDeleted != true).AsQueryable();
                if (query.ChemistId != null)
                {
                    dbQuery = dbQuery.Where(x => x.ChemistId == query.ChemistId);
                }
            }
            return new GetChemistAssignedGeoZoneKeyValueQueryResponse
            {
                GeoZones = dbQuery.Select(x => new ChemistAssignedGeoZoneKeyValueDto
                {
                    Id = x.ChemistAssignedGeoZoneId,
                    GeoZoneId=x.GeoZoneId,
                    Name = x.NameEn
                }).ToList(),
            } as IGetChemistAssignedGeoZoneKeyValueQueryResponse;
        }
    }
}
