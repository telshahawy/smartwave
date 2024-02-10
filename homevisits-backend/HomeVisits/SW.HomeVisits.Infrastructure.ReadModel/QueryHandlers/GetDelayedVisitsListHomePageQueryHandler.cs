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
    public class GetDelayedVisitsListHomePageQueryHandler : IQueryHandler<IGetDelayedVisitsListHomePageQuery, ISearchVisitsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
       
        public GetDelayedVisitsListHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public ISearchVisitsQueryResponse Read(IGetDelayedVisitsListHomePageQuery query)
        {
            IQueryable<VisitsHomePageView> dbQuery = _context.VisitsHomePageViews;
            IQueryable<TimeZoneFramesView> timeQuery = _context.TimeZoneFramesViews;
            var HomePageVisits = dbQuery;
            var otherVisits = dbQuery;
            if (query.GeoZoneId == Guid.Empty)
            {
                HomePageVisits = dbQuery.Where(x => x.VisitDate.Date == DateTime.Today);

            }
            else
            {
                HomePageVisits = dbQuery.Where(x => x.VisitDate.Date == DateTime.Today && x.GeoZoneId == query.GeoZoneId);
                otherVisits = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId);

            }

            var DelayedVisits = otherVisits
            .Where(x => x.VisitDate.Date > DateTime.Today
                    && (x.VisitStatusTypeId == (int)VisitStatusTypes.Confirmed
                    ||  x.ChemistId == null || x.VisitStatusTypeId == (int)VisitStatusTypes.Reject)).OrderByDescending(o => o.VisitDate);
            

            var totalCount = DelayedVisits.Count();

            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                HomePageVisits = HomePageVisits.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchVisitsQueryResponse()
            {
                Visits = DelayedVisits.Select(v => new VisitsDto
                {
                    VisitId = v.VisitId,
                    VisitNo = v.VisitNo,
                    VisitDate = v.VisitDate.ToString("yyyy/MM/dd"),
                    PatientName = v.PatientName,
                    PatientNo = v.PatientNo,
                    Gender = v.Gender,
                    GenderName = query.cultureName == CultureNames.ar ? v.Gender == 1 ? "ذكر" : "انثى" : v.Gender == 1 ? "Male" : "Female",
                    DOB = v.DOB,
                    PhoneNumber = v.PhoneNumber,
                    GeoZoneName = query.cultureName == CultureNames.ar ? v.GeoZoneNameAr : v.GeoZoneNameEn,
                    ChemistName = v.ChemistName,
                    StatusName = query.cultureName == CultureNames.ar ? v.StatusNameAr : v.StatusNameEn,
                    GeoZoneId = v.GeoZoneId,
                    TimeSlot = $"{new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().StartTime.Ticks).ToString("hh:mm tt")} : {new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt")}",
                    StartTime = new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().StartTime.Ticks).ToString("hh:mm tt"),
                    EndTime = new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt")

                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchVisitsQueryResponse;

        }
    }
}
