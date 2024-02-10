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
    public class GetGovernateForEditQueryHandler : IQueryHandler<IGetGovernateForEditQuery, IGetGovernateForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetGovernateForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetGovernateForEditQueryResponse Read(IGetGovernateForEditQuery query)
        {
            IQueryable<GovernateView> dbQuery = _context.GovernateViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var governate = dbQuery.SingleOrDefault(x => x.GovernateId == query.GovernateId);
            if (governate == null)
            {
                throw new Exception("Governate not found");
            }
            return new GetGovernateForEditQueryResponse
            {
                Governate = new GovernatsDto
                {
                    GovernateId = governate.GovernateId,
                    CountryId = governate.CountryId,
                    GovernateName = governate.GoverNameEn,
                    Code = governate.Code,
                    CustomerServiceEmail = governate.CustomerServiceEmail,
                    IsActive = governate.IsActive,
                    IsDeleted = governate.IsDeleted
                }
            
            } as IGetGovernateForEditQueryResponse;
        }
    }
}

