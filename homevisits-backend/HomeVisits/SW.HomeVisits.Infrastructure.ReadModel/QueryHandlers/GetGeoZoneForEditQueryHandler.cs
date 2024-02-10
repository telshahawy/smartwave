using System;
using System.Linq;
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
    public class GetGeoZoneForEditQueryHandler : IQueryHandler<IGetGeoZoneForEditQuery, IGetGeoZoneForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetGeoZoneForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetGeoZoneForEditQueryResponse Read(IGetGeoZoneForEditQuery query)
        {
            IQueryable<GeoZoneView> dbQuery = _context.GeoZoneView;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId);
            }

            var geoZones = dbQuery.ToList();
            var timeZoneFrames = geoZones.GroupJoin(_context.TimeZoneFramesViews.AsQueryable(),  //inner sequence
                                          geozone => geozone.GeoZoneId, //outerKeySelector 
                                          tz => tz.GeoZoneId,     //innerKeySelector
                                          (geozone, timeFramesCollection) => new // resultSelector 
                                          {
                                              geozone,
                                              TimeZoneFrames = timeFramesCollection,
                                          });

            return new GetGeoZoneForEditQueryResponse
            {
                GeoZone = timeZoneFrames.Select(p => new GeoZonesDto
                {
                    GeoZoneId = p.geozone.GeoZoneId,
                    Code = p.geozone.Code,
                    GeoZoneName = p.geozone.NameEn,
                    GovernateId = p.geozone.governateId,
                    CountryId = p.geozone.CountryId,
                    MappingCode = p.geozone.MappingCode,
                    IsActive = p.geozone.IsActive,
                    KmlFilePath = p.geozone.KmlFilePath,
                    KmlFileName=p.geozone.KmlFileName,
                    TimeZoneFrames = p.TimeZoneFrames.Where(x=>x.IsDeleted != true).OrderBy(x=>x.StartTime).Select(tz => new TimeZoneFramesDto
                    {
                        TimeZoneFrameId = tz.TimeZoneFrameId,
                        GeoZoneId = tz.GeoZoneId,
                        Name = tz.NameEN,
                        VisitsNoQuota = tz.VisitsNoQouta,
                        StartTime = new DateTime(tz.StartTime.Ticks).ToString("hh:mm tt"),
                        EndTime = new DateTime(tz.EndTime.Ticks).ToString("hh:mm tt"),
                        StartTimeValue = tz.StartTime,
                        EndTimeValue = tz.EndTime,
                        IsDeleted = tz.IsDeleted,
                        BranchDispatch = tz.BranchDispatch
                    })
                }).FirstOrDefault()

            } as IGetGeoZoneForEditQueryResponse;
        }
    }
}

