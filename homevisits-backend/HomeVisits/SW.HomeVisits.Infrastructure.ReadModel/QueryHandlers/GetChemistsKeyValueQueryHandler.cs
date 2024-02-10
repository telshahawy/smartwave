using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class GetChemistsKeyValueQueryHandler : IQueryHandler<IGetChemistsKeyValueQuery, IGetChemistsKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistsKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistsKeyValueQueryResponse Read(IGetChemistsKeyValueQuery query)
        {
            IQueryable<ChemistAssignedGeoZonesView> dbQuery = _context.ChemistAssignedGeoZonesViews;
            dbQuery = dbQuery.Where(x => x.ClientId == query.ClientId && x.IsActive == true && x.IsDeleted != true).AsQueryable();

            if (query != null)
            {
                if (query.GeoZoneId != null)
                {
                    dbQuery = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId);
                }
            }

            return new GetChemistsKeyValueQueryResponse()
            {
                Chemists = dbQuery.Select(x => new ChemistKeyValueDto
                {
                    ChemistId = x.ChemistId,
                    Name = x.ChemistName
                }).Distinct().ToList()
            } as IGetChemistsKeyValueQueryResponse;
        }
    }
}
