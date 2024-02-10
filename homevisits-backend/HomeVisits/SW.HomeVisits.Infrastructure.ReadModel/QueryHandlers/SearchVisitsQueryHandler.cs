using System;
using System.Data.Entity;
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
    public class SearchVisitsQueryHandler : IQueryHandler<ISearchVisitsQuery, ISearchVisitsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchVisitsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchVisitsQueryResponse Read(ISearchVisitsQuery query)
        {
            IQueryable<VisitsView> dbQuery = _context.VisitsViews;

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => (query.ClientId == null || x.ClientId == query.ClientId) &&
                (query.GovernateId == null || x.GovernateId == query.GovernateId) &&
                (query.GeoZoneId == null || x.GeoZoneId == query.GeoZoneId) &&
                (query.TimeZoneGeoZoneId == null || x.TimeZoneGeoZoneId == query.TimeZoneGeoZoneId) &&
                (query.VisitDate == null || x.VisitDate.Date == query.VisitDate.Value.Date) &&
                (query.VisitDateFrom == null || x.VisitDate.Date >= query.VisitDateFrom.Value.Date) &&
                (query.VisitDateTo == null || x.VisitDate.Date <= query.VisitDateTo.Value.Date) &&
                (query.TimeZoneStartTime == null || x.StartTime >= query.TimeZoneStartTime.Value) &&
                (query.TimeZoneEndTime == null || x.EndTime <= query.TimeZoneEndTime.Value) &&
                (query.VisitNoFrom == null || x.VisitCode >= query.VisitNoFrom) &&
                (query.VisitNoTo == null || x.VisitCode <= query.VisitNoTo) &&
                (query.CreationDateFrom == null || x.CreatedDate.Date >= query.CreationDateFrom.Value.Date) &&
                (query.CreationDateTo == null || x.CreatedDate.Date <= query.CreationDateTo.Value.Date) &&
                (string.IsNullOrEmpty(query.PatientNo) || x.PatientNo == query.PatientNo) &&
                (string.IsNullOrEmpty(query.PatientName) || x.PatientName == query.PatientName) &&
                (query.Gender == null || x.Gender == query.Gender) &&
                (string.IsNullOrEmpty(query.PatientMobileNo) || x.PhoneNumber == query.PatientMobileNo) &&
                (query.VisitStatusTypeId == null || x.VisitStatusTypeId == query.VisitStatusTypeId) &&
                (query.NeedExpert == null || x.NeedExpert == query.NeedExpert) &&
                (query.AssignedTo == null || x.ChemistId == query.AssignedTo) &&
                (query.AssignStatus == null || (query.AssignStatus == 1 ? x.ChemistId != null : query.AssignStatus == 2 ? x.ChemistId == null
                : query.AssignStatus == 3 ? x.VisitStatusTypeId == (int)VisitStatusTypes.Reject : true))
                ).OrderByDescending(o => o.VisitCode);


            //Sorting
            if (query.SortBy != null)
            {
                if (query.SortBy == 1)// Visit Date Ascending
                {
                    dbQuery = dbQuery.OrderBy(o => o.VisitDate);
                }
                else if (query.SortBy == 3)// Creation Date Ascending
                {
                    dbQuery = dbQuery.OrderBy(o => o.CreatedDate);
                }
                else if (query.SortBy == 4)// Creation Date Descending
                {
                    dbQuery = dbQuery.OrderByDescending(o => o.CreatedDate);
                }
            }

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchVisitsQueryResponse()
            {
                Visits = dbQuery.Select(v => new VisitsDto
                {
                    VisitId = v.VisitId,
                    VisitNo = v.VisitNo,
                    VisitCode = v.VisitCode,
                    VisitDate = v.VisitDate.ToString("yyyy/MM/dd"),
                    VisitDateValue = v.VisitDate,
                    PatientName = v.PatientName,
                    PatientNo = v.PatientNo,
                    Gender = v.Gender,
                    GenderName = query.cultureName == CultureNames.ar ? v.Gender == 1 ? "ذكر" : "انثى" : v.Gender == 1 ? "Male" : "Female",
                    DOB = v.DOB,
                    PhoneNumber = v.PhoneNumber,
                    GeoZoneName = query.cultureName == CultureNames.ar ? v.GeoZoneNameAr : v.GeoZoneNameEn,
                    ChemistName = v.ChemistName,
                    ChemistId = v.ChemistId,
                    StatusName = query.cultureName == CultureNames.ar ? v.StatusNameAr : v.StatusNameEn,
                    GeoZoneId = v.GeoZoneId,
                    TimeSlot = $"{new DateTime(v.StartTime.Ticks).ToString("hh:mm tt")}:{new DateTime(v.EndTime.Ticks).ToString("hh:mm tt")}",//$"{new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().StartTime.Ticks).ToString("hh:mm tt")} : {new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt")}",
                    StartTime = new DateTime(v.StartTime.Ticks).ToString("hh:mm tt"),//new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().StartTime.Ticks).ToString("hh:mm tt"),
                    EndTime = new DateTime(v.EndTime.Ticks).ToString("hh:mm tt"),//new DateTime(timeQuery.Where(x => x.TimeZoneFrameId == v.TimeZoneGeoZoneId).FirstOrDefault().EndTime.Ticks).ToString("hh:mm tt")
                    TimeZoneStartTime = v.StartTime,
                    TimeZoneEndTime = v.EndTime,
                    VisitStatusTypeId = v.VisitStatusTypeId,
                    VisitTypeId = v.VisitTypeId,
                    Latitude = v.Latitude,
                    Longitude = v.Longitude,
                    ClientId = v.ClientId,
                    TimeZoneFrameId = v.TimeZoneGeoZoneId,
                    VisitsNoQouta = v.VisitsNoQouta,
                    CreatedDate = v.CreatedDate,
                    VisitTime = v.VisitTime,
                    IamNotSure = v.IamNotSure,
                    RelativeDateOfBirth = v.RelativeDateOfBirth,
                    RelativeAgeSegmentId = v.RelativeAgeSegmentId
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchVisitsQueryResponse;
        }
    }
}
