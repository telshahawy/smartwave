using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetCanceledVisitReportQueryHandler : IQueryHandler<IGetCanceledVisitReportQuery, IGetCanceledVisitReportQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetCanceledVisitReportQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetCanceledVisitReportQueryResponse Read(IGetCanceledVisitReportQuery query)
        {
            IQueryable<VisitDetailsReasonReportView> dbQuery = _context.visitDetailsReasonReportViews;
            IQueryable<UserView> userQuery = _context.UserViews;
            IQueryable<CountryView> countryQuery = _context.CountryViews;
            IQueryable<GovernateView> govQuery = _context.GovernateViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            IQueryable<ReasonsView> reasonQuery = _context.ReasonsViews;
            var cancelledVisit = dbQuery;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            cancelledVisit = dbQuery.Where(x => x.VisitDate.Date >= query.VisitDateFrom && x.VisitDate.Date <= query.VisitDateTo
              && x.VisitStatusTypeId == (int)VisitStatusTypes.Cancelled && x.VisitActionTypeId == (int)VisitActionTypes.Cancelled
                     && (query.CountryOption == Guid.Empty || x.CountryId == query.CountryOption)
                     && (query.GovernorateOption == Guid.Empty || x.GovernateId == query.GovernorateOption)
                     && (query.AreaOption == Guid.Empty || x.GeoZoneId == query.AreaOption)
                     && (query.CancellationReason == -1 || query.CancellationReason == null || x.ReasonId == query.CancellationReason)
                   );
            var country = query.CountryOption == Guid.Empty ? "All" : query.cultureName == CultureNames.ar ? countryQuery.Where(x => x.CountryId == query.CountryOption).FirstOrDefault().CountryNameAr : countryQuery.Where(x => x.CountryId == query.CountryOption).FirstOrDefault().CountryNameEn;
            var gov = query.GovernorateOption == Guid.Empty ? "All" : query.cultureName == CultureNames.ar ? govQuery.Where(x => x.GovernateId == query.GovernorateOption).FirstOrDefault().GoverNameAr : govQuery.Where(x => x.GovernateId == query.GovernorateOption).FirstOrDefault().GoverNameEn;
            var area = query.AreaOption == Guid.Empty ? "All" : query.cultureName == CultureNames.ar ? geoQuery.Where(x => x.GeoZoneId == query.AreaOption).FirstOrDefault().NameAr : geoQuery.Where(x => x.GeoZoneId == query.AreaOption).FirstOrDefault().NameEn;
            var reason = query.CancellationReason == -1 ? "All" : reasonQuery.Where(x => x.ReasonId == query.CancellationReason).FirstOrDefault().ReasonName;
            var userName = userQuery.Where(x => x.UserId == query.UserId).FirstOrDefault().Name;
            cancelledVisit = cancelledVisit.OrderBy(o => o.VisitDate);
            var visitNo = cancelledVisit.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                cancelledVisit = cancelledVisit.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new GetCanceledVisitReportQueryResponse
            {
                DateFrom = query.VisitDateFrom.ToString("yyyy/MM/dd hh:mm tt"),
                DateTo = query.VisitDateTo.ToString("yyyy/MM/dd hh:mm tt"),
                Country = country,
                Governorate = gov,
                Area = area,
                reason = reason,
                PrintedBy = userName,
                PrintedDate = DateTime.UtcNow.ToString("yyyy/MM/dd hh:mm tt"),
                canceledVisitReports = cancelledVisit.Select(c => new CanceledVisitReportDto
                {
                    VisitId = c.VisitNo.ToString(),
                    VisitDate = c.VisitDate.ToString("yyyy/MM/dd hh:mm tt"),
                    PatientName = c.Name,
                    MobileNumber = c.PatientPhone,
                    Age = c.DOB,
                    Area = c.ZoneNameEn,
                    CancellationReason = c.CancelReason,
                    CancellationTime = c.ActionCreationDate.ToString("yyyy/MM/dd hh:mm tt"),
                    CancelledBy = userQuery.Where(x => x.UserId == c.CreatedBy).FirstOrDefault().Name,
                    Gender = c.Gender == (int)GenderTypes.Male ? "Male" : c.Gender == (int)GenderTypes.Female ? "Female" : "UnKnown"
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = visitNo,
                PageSize = query.PageSize
            } as IGetCanceledVisitReportQueryResponse;

        }
    }
}
