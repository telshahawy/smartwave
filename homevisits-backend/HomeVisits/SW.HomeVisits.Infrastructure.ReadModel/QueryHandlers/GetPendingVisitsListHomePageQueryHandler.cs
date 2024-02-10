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
    public class GetPendingVisitsListHomePageQueryHandler : IQueryHandler<IGetPendingVisitsListHomePageQuery, ISearchVisitsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetPendingVisitsListHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public ISearchVisitsQueryResponse Read(IGetPendingVisitsListHomePageQuery query)
        {
            IQueryable<VisitsHomePageView> dbQuery = _context.VisitsHomePageViews;
            IQueryable<TimeZoneFramesView> timeQuery = _context.TimeZoneFramesViews;
            var HomePageVisits = dbQuery;
            
            if (query.GeoZoneId == Guid.Empty)
            {
                HomePageVisits = dbQuery.Where(x => x.VisitDate.Date == DateTime.Today);

            }
            else
            {
                HomePageVisits = dbQuery.Where(x => x.VisitDate.Date == DateTime.Today && x.GeoZoneId == query.GeoZoneId);
              

            }

            var PendingVisits = HomePageVisits.Where(x => x.ChemistId == null || x.VisitStatusTypeId == (int)VisitStatusTypes.Reject).OrderByDescending(o => o.VisitDate);

            var totalCount = PendingVisits.Count();

            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                HomePageVisits = HomePageVisits.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchVisitsQueryResponse()
            {
                Visits = PendingVisits.Select(v => new VisitsDto
                {
                    VisitId = v.VisitId,
                    VisitNo = v.VisitNo,
                    VisitDate = v.VisitDate.ToString("yyyy/MM/dd"),
                    PatientName = v.PatientName,
                    PatientNo = v.PatientNo,
                    Gender = v.Gender,
                    GenderName = query.cultureName == CultureNames.ar ? v.Gender == 1 ? "ذكر" : "انثى" : v.Gender == 1 ? "Male" : "Female",
                    DOB = v.DOB,
                    VisitCode=v.VisitCode,
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
