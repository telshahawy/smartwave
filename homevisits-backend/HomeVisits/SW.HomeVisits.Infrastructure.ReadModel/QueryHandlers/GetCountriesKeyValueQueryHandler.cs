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
    public class GetCountriesKeyValueQueryHandler : IQueryHandler<IGetCountriesKeyValueQuery, IGetCountriesKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetCountriesKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetCountriesKeyValueQueryResponse Read(IGetCountriesKeyValueQuery query)
        {
            IQueryable<CountryView> dbQuery = _context.CountryViews;
            if (query != null && query.CultureName != null)
            {

                dbQuery = dbQuery.Where(x=>x.ClientId == query.ClientId && x.IsActive && !x.IsDeleted).AsQueryable() ;
            }

            return new GetCountriesKeyValueQueryResponse()
            {
                Countries = dbQuery.Select(x => new CountryKeyValueDto
                {
                    CountryId = x.CountryId,
                    Name = query.CultureName == CultureNames.ar ? x.CountryNameAr : x.CountryNameEn
                }).OrderBy(o => o.CountryId).ToList()
            } as IGetCountriesKeyValueQueryResponse;
        }
    }
}
