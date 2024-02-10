using Common.Logging;
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
    public class SearchPatientScheduleQueryHandler : IQueryHandler<ISearchPatientScheduleQuery, ISearchPatientScheduleQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchPatientScheduleQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchPatientScheduleQueryResponse Read(ISearchPatientScheduleQuery query)
        {
            IQueryable<VisitsView> dbQuery = _context.VisitsViews;
            IQueryable<TimeZoneFramesView> timeQuery = _context.TimeZoneFramesViews;

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.PatientId == query.PatientId &&
            (query.ClientId == Guid.Empty || x.ClientId == query.ClientId) &&
               (query.VisitDate == null || x.VisitDate == query.VisitDate) &&
               (query.VisitStatusTypeId == null || x.VisitStatusTypeId == query.VisitStatusTypeId)
                ).OrderByDescending(o => o.VisitCode);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchPatientScheduleQueryResponse()
            {
                TodaysVisits = dbQuery.Where(x => x.VisitDate.Date == DateTime.Now.Date).Select(v => new VisitsDto
                {
                    VisitId = v.VisitId,
                    VisitNo = v.VisitNo,
                    VisitCode = v.VisitCode,
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
                    EndTime = new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt"),
                    ChemistId = v.ChemistId,
                    ClientId = v.ClientId,
                    CreatedDate = v.CreatedDate,
                    VisitTypeId = v.VisitTypeId,
                    VisitStatusTypeId = v.VisitStatusTypeId,
                    VisitDateValue = v.VisitDate,
                    VisitsNoQouta = v.VisitsNoQouta,
                    TimeZoneFrameId = v.TimeZoneGeoZoneId
                }).ToList(),
                OldVisits = dbQuery.Where(x => x.VisitDate < DateTime.Now).Select(v => new VisitsDto
                {
                    VisitId = v.VisitId,
                    VisitNo = v.VisitNo,
                    VisitCode = v.VisitCode,
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
                    EndTime = new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt"),
                    ChemistId = v.ChemistId,
                    ClientId = v.ClientId,
                    CreatedDate = v.CreatedDate,
                    VisitTypeId = v.VisitTypeId,
                    VisitStatusTypeId = v.VisitStatusTypeId,
                    VisitDateValue = v.VisitDate,
                    VisitsNoQouta = v.VisitsNoQouta,
                    TimeZoneFrameId = v.TimeZoneGeoZoneId
                }).ToList(),

                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchPatientScheduleQueryResponse;
        }


    }
}
