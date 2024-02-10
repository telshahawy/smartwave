using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
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
    public class GetTimeZoneFramesQueryHandler : IQueryHandler<IGetTimeZoneFramesQuery, IGetTimeZoneFramesQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;

        public GetTimeZoneFramesQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;

        }
        public IGetTimeZoneFramesQueryResponse Read(IGetTimeZoneFramesQuery query)
        {
            IQueryable<TimeZoneFramesView> dbQuery = _context.TimeZoneFramesViews;

            if (query != null)
            {
                if (query.TimeZoneFrameId.HasValue)
                    dbQuery = dbQuery.Where(x => x.TimeZoneFrameId == query.TimeZoneFrameId.Value);
                if (query.VisitTime.HasValue)
                    dbQuery = dbQuery.Where(x => x.StartTime <= query.VisitTime.Value && x.EndTime >= query.VisitTime.Value);
                if (query.GeoZoneId.HasValue)
                    dbQuery = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId.Value);
                if (query.IsDeleted.HasValue)
                    dbQuery = dbQuery.Where(x => x.IsDeleted == query.IsDeleted.Value);
            }

            return new GetTimeZoneFramesQueryResponse
            {
                TimeZoneFramesDto = dbQuery.OrderBy(x => x.StartTime).Select(p => new TimeZoneFramesDto
                {
                    TimeZoneFrameId = p.TimeZoneFrameId,
                    StartTime = new DateTime(p.StartTime.Ticks).ToString("hh:mm tt"),
                    EndTime = new DateTime(p.EndTime.Ticks).ToString("hh:mm tt"),
                    BranchDispatch = p.BranchDispatch,
                    GeoZoneId = p.GeoZoneId,
                    IsDeleted = p.IsDeleted,
                    Name = query.CultureName == CultureNames.ar ? p.NameAr : p.NameEN,
                    EndTimeValue = p.EndTime,
                    StartTimeValue = p.StartTime,
                    VisitsNoQuota = p.VisitsNoQouta
                }).ToList()
            } as IGetTimeZoneFramesQueryResponse;

        }
    }
}
