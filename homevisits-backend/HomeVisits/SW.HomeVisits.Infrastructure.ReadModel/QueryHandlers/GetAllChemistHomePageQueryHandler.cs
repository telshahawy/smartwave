using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetAllChemistHomePageQueryHandler : IQueryHandler<IGetAllChemistHomePageQuery, IGetAllChemistHomePageQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetAllChemistHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetAllChemistHomePageQueryResponse Read(IGetAllChemistHomePageQuery query)
        {
            IQueryable<AllChemistHomePageView> dbQuery = _context.AllChemistHomePageViews ;
            var chemistNo = 0;

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            if (query.GeoZoneId != Guid.Empty)
            {
                chemistNo = dbQuery.Count(x => x.GeoZoneId == query.GeoZoneId);

            }
            else
            {
                chemistNo = dbQuery.Count();
            }

            //if (chemistNo == null)
            //{
            //    throw new Exception("Areas not found");
            //}

            return new GetAllChemistHomePageQueryResponse
            {
                No=chemistNo

            } as IGetAllChemistHomePageQueryResponse;

        }
    }
}
