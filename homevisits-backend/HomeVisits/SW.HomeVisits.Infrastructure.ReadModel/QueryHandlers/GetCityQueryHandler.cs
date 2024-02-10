using System;
using Common.Logging;
using SW.Framework.Cqrs;
using System.Linq;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetCityQueryHandler: IQueryHandler<IGetCityQuery, IGetCityQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetCityQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetCityQueryResponse Read(IGetCityQuery query)
        {
            IQueryable<CityView> dbQuery = _context.CityViews;
            if (query != null)
            {
                    dbQuery = dbQuery.Where(x => x.CityId == query.CityId);
            }

            return new GetCityQueryResponse() { city = dbQuery.Select(x=> new CityDto
            {
                CityId = x.CityId,
                NameAr = x.NameAr,
                NameEn = x.NameEn
            }).SingleOrDefault() } as IGetCityQueryResponse;
        }
    }
}
