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
    public class GetTimeZonesForGeoZoneQueryHandler : IQueryHandler<IGetGeoZoneForEditQuery, IGetTimeZonesForGeoZoneQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;


        public GetTimeZonesForGeoZoneQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;

        }
        public IGetTimeZonesForGeoZoneQueryResponse Read(IGetGeoZoneForEditQuery query)
        {
            IQueryable<TimeZoneFramesView> dbQuery = _context.TimeZoneFramesViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId && x.IsDeleted == false);
            }
            return new GetTimeZonesForGeoZoneQueryResponse
            {
                TimeZonesForGeoZone = dbQuery.OrderBy(x=>x.StartTime).Select(p => new TimeZonesForGeoZoneDto
                {
                    TimeZoneFrameId = p.TimeZoneFrameId,
                    StartTime = p.StartTime,//.Hours,.Minutes
                    EndTime = p.EndTime
                    
                }).ToList()
            } as IGetTimeZonesForGeoZoneQueryResponse;

        }
    }
}

