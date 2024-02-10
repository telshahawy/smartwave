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
    public class SearchChemistVisitsOrderQueryHandler : IQueryHandler<ISearchChemistVisitsOrderQuery, ISearchChemistVisitsOrderQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchChemistVisitsOrderQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchChemistVisitsOrderQueryResponse Read(ISearchChemistVisitsOrderQuery query)
        {
            IQueryable<ChemistVisitsOrderView> dbQuery = _context.ChemistVisitsOrderViews;
            IQueryable<TimeZoneFramesView> timeQuery = _context.TimeZoneFramesViews;

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => (query.ChemistId == null || x.ChemistId == query.ChemistId) &&
                (query.ChemistVisitOrderId == null || x.ChemistVisitOrderId == query.ChemistVisitOrderId) &&
                (query.TimeZoneFrameId == null || x.TimeZoneGeoZoneId == query.TimeZoneFrameId.Value) &&
                (query.VisitId == null || x.VisitId == query.VisitId.Value)&&
                (query.ClientId == null || x.ClientId == query.ClientId.Value) &&
                (query.VisitCreatedDate == null || x.VisitOrderCreatedDate == query.VisitCreatedDate) &&
                (query.VisitOrderCreatedDate == null || x.VisitOrderCreatedDate == query.VisitOrderCreatedDate) &&
                (query.IsDeleted == null || x.IsDeleted == query.IsDeleted) &&
                (query.VisitDate == null || x.VisitDate.Date == query.VisitDate.Value.Date) &&
                (query.VisitDateFrom == null || x.VisitDate.Date >= query.VisitDateFrom.Value.Date) &&
                (query.VisitDateTo == null || x.VisitDate.Date <= query.VisitDateTo.Value.Date)
                ).OrderBy(o => o.ChemistId).ThenBy(p => p.TimeZoneGeoZoneId).ThenBy(p => p.VisitOrder);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchChemistVisitsOrderQueryResponse()
            {
                Visits = dbQuery.Select(v => new ChemistVisitsOrderDto
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
                    ChemistVisitOrderId = v.ChemistVisitOrderId,
                    CreatedDate = v.VisitOrderCreatedDate,
                    VisitOrder = v.VisitOrder,
                    IsDeleted = v.IsDeleted,
                    StartLangitude = v.StartLangitude,
                    StartLatitude = v.StartLatitude,
                    Distance = v.Distance,
                    Duration = v.Duration,
                    DurationInTraffic = v.DurationInTraffic
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchChemistVisitsOrderQueryResponse;
        }
    }
}
