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
    public class GetRejectedVisitReportQueryHandler : IQueryHandler<IGetRejectedVisitReportQuery, IGetRejectedVisitReportQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetRejectedVisitReportQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetRejectedVisitReportQueryResponse Read(IGetRejectedVisitReportQuery query)
        {
            IQueryable<VisitDetailsReasonReportView> dbQuery = _context.visitDetailsReasonReportViews;
            IQueryable<UserView> userQuery = _context.UserViews;
            IQueryable<CountryView> countryQuery = _context.CountryViews;
            IQueryable<GovernateView> govQuery = _context.GovernateViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            IQueryable<ChemistsView> chemistQuery = _context.ChemistsViews;
            IQueryable<ReasonsView> reasonQuery = _context.ReasonsViews;

            var rejectVisit = dbQuery.Where(x => x.VisitDate >= query.VisitDateFrom && x.VisitDate <= query.VisitDateTo
               && (query.CountryOption == Guid.Empty || x.CountryId == query.CountryOption)
               && (query.GovernorateOption == Guid.Empty || x.GovernateId == query.GovernorateOption)
               && (query.AreaOption == Guid.Empty || x.GeoZoneId == query.AreaOption)
               && (query.ChemistOption == Guid.Empty || x.ChemistId == query.ChemistOption)
               && (query.Reason == -1 || query.Reason == null || x.ReasonId == query.Reason)
               && (x.VisitActionTypeId == (int)VisitActionTypes.ReassignChemist || x.VisitActionTypeId == (int)VisitActionTypes.Cancelled)
               //&& (x.VisitStatusTypeId == (int)VisitStatusTypes.Reject || x.VisitStatusTypeId == (int)VisitStatusTypes.Cancelled)
               );

            //if (query.DelayedOption.ToLower() == "yes")
            //{
            //    rejectVisit = dbQuery.Where(p => (p.VisitDate.Date < DateTime.Now.Date && p.VisitStatusTypeId != (int)VisitStatusTypes.Done && p.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled) || p.VisitStatusCreationDate.Date > p.VisitDate.Date
            //             || (p.VisitStatusCreationDate.Date == p.VisitDate.Date && p.VisitStatusCreationDate.TimeOfDay > p.EndTime));
            //}
            //else if (query.DelayedOption.ToLower() == "no")
            //{
            //    rejectVisit = dbQuery.Where(x => x.VisitDate.Date > DateTime.Now.Date || (x.VisitDate == DateTime.Now.Date && x.EndTime > DateTime.Now.TimeOfDay) ||
            //            ((x.VisitStatusTypeId == (int)VisitStatusTypes.Done || x.VisitStatusTypeId == (int)VisitStatusTypes.Cancelled) && (x.VisitStatusCreationDate.Date < x.VisitDate.Date || (x.VisitStatusCreationDate.Date == x.VisitDate.Date && x.VisitStatusCreationDate.TimeOfDay < x.EndTime)))
            //      );
            //}

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            rejectVisit = rejectVisit.OrderBy(o => o.VisitDate);

            var country = query.CountryOption == Guid.Empty ? "All" : query.cultureName == CultureNames.ar ? countryQuery.Where(x => x.CountryId == query.CountryOption).FirstOrDefault().CountryNameAr : countryQuery.Where(x => x.CountryId == query.CountryOption).FirstOrDefault().CountryNameEn;
            var gov = query.GovernorateOption == Guid.Empty ? "All" : query.cultureName == CultureNames.ar ? govQuery.Where(x => x.GovernateId == query.GovernorateOption).FirstOrDefault().GoverNameAr : govQuery.Where(x => x.GovernateId == query.GovernorateOption).FirstOrDefault().GoverNameEn;
            var area = query.AreaOption == Guid.Empty ? "All" : query.cultureName == CultureNames.ar ? geoQuery.Where(x => x.GeoZoneId == query.AreaOption).FirstOrDefault().NameAr : geoQuery.Where(x => x.GeoZoneId == query.AreaOption).FirstOrDefault().NameEn;
            var reason = query.Reason == -1 ? "All" : reasonQuery.Where(x => x.ReasonId == query.Reason).FirstOrDefault().ReasonName;
            var userName = userQuery.Where(x => x.UserId == query.UserId).FirstOrDefault().Name;
            var chemist = query.ChemistOption == Guid.Empty ? "All" : chemistQuery.Where(x => x.ChemistId == query.ChemistOption).FirstOrDefault().Name;

            int totalNo = rejectVisit.Count();
            int cancelledNo = rejectVisit.Where(x => x.VisitActionTypeId == (int)VisitActionTypes.Cancelled).Count();
            int reAssignedNo = rejectVisit.Where(x => x.VisitActionTypeId == (int)VisitActionTypes.ReassignChemist).Count();

            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                rejectVisit = rejectVisit.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new GetRejectedVisitReportQueryResponse
            {
                Country = country,
                Governorate = gov,
                Area = area,
                Chemist = chemist,
                DateFrom = query.VisitDateFrom.ToString("yyyy/MM/dd  hh:mm tt"),
                DateTo = query.VisitDateTo.ToString("yyyy/MM/dd  hh:mm tt"),
                Delayed = query.DelayedOption,
                TotalVisitsNo = totalNo,
                PrintedBy = userName,
                CancelledVisitsNo = cancelledNo,
                ReassignedVisitsNo = reAssignedNo,
                Reason = reason,
                PrintedDate = DateTime.UtcNow.ToString("yyyy/MM/dd  hh:mm tt"),
                RejectedVisitReports = rejectVisit.Select(u => new RejectedVisitReportDto
                {
                    VisitDate = u.VisitDate.ToString("yyyy/MM/dd  hh:mm tt"),
                    VisitId = u.VisitNo,
                    Area = query.cultureName == CultureNames.en ? u.ZoneNameEn : u.ZoneNameAr,
                    ChemistName = u.ChemistName,
                    Reason = u.CancelReason,
                    MobileNumber = u.PatientPhone,
                    PatientName = u.Name,
                    RejectionType = u.VisitActionTypeId == (int)VisitActionTypes.Cancelled ? "Cancel" : u.VisitActionTypeId == (int)VisitActionTypes.ReassignChemist ? "Re-assign" : "UnKnown"

                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalNo,
                PageSize = query.PageSize
            } as IGetRejectedVisitReportQueryResponse;

        }
    }
}
