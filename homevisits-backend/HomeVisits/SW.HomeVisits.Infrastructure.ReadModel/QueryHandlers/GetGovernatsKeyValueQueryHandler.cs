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
    public class GetGovernatsKeyValueQueryHandler : IQueryHandler<IGetGovernatsKeyValueQuery, IGetGovernatsKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetGovernatsKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetGovernatsKeyValueQueryResponse Read(IGetGovernatsKeyValueQuery query)
        {
            IQueryable<GovernateView> dbQuery = _context.GovernateViews;
            if (query != null && query.CultureName != null)
            {

                dbQuery = dbQuery.Where(x=>x.ClientId == query.ClientId && x.IsActive && !x.IsDeleted && x.CountryIsActive == true && x.CountryIsDeleted != true).AsQueryable();
                if (query.CountryId != null && query.CountryId != null)
                {
                    dbQuery = dbQuery.Where(x => x.CountryId == query.CountryId);
                }
            }

            return new GetGovernatsKeyValueQueryResponse()
            {
                Governats = dbQuery.Select(x => new GovernatsKeyValueDto
                {
                    GovernateId = x.GovernateId,
                    Name = query.CultureName == CultureNames.ar ? x.GoverNameAr : x.GoverNameEn
                }).OrderBy(o => o.GovernateId).ToList()
            } as IGetGovernatsKeyValueQueryResponse;
        }
    }
}
