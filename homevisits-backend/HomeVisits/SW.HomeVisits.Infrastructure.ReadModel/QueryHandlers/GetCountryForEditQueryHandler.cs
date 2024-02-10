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
    public class GetCountryForEditQueryHandler : IQueryHandler<IGetCountryForEditQuery, IGetCountryForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetCountryForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetCountryForEditQueryResponse Read(IGetCountryForEditQuery query)
        {
            IQueryable<CountryView> dbQuery = _context.CountryViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var country = dbQuery.SingleOrDefault(x => x.CountryId == query.CountryId);
            if (country == null)
            {
                throw new Exception("Country not found");
            }
            return new GetCountryForEditQueryResponse
            {
                Country = new CountriesDto
                {
                    CountryId = country.CountryId,
                    CountryName = country.CountryNameEn,
                    Code = country.Code,
                    MobileNumberLength = country.MobileNumberLength,
                    IsActive = country.IsActive,
                    IsDeleted = country.IsDeleted
                }
            
            } as IGetCountryForEditQueryResponse;
        }
    }
}

