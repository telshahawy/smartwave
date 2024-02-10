using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
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
    class GetUserAreasForHomePageQueryHandler : IQueryHandler<IGetUserAreasForHomePageQuery, IGetUserAreasForHomePageQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetUserAreasForHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetUserAreasForHomePageQueryResponse Read(IGetUserAreasForHomePageQuery query)
        {
            IQueryable<UserAreasView> dbQuery = _context.UserAreasViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var userAreas = dbQuery.Where(x => x.UserId == query.UserId && x.UserGeoActive==true &&x.UserGeoDeleted==false
            &&x.GeoActive==true &&x.GeoDeleted==false).ToList();//Logic error????

            if (userAreas == null)
            {
                throw new Exception("Areas not found");
            }

            return new GetUserAreasForHomePageQueryResponse
            {
                UserAreas = userAreas.GroupBy(U=>U.NameEn).Select(U => new UserAreasDto
                {
                    GeoZoneId =U.First().GeoZoneId,
                    GeoZoneNameAr=U.First().NameAr,
                    GeoZoneNameEn=U.First().NameEn
                }).ToList()

            } as IGetUserAreasForHomePageQueryResponse;
        }
    }
}
