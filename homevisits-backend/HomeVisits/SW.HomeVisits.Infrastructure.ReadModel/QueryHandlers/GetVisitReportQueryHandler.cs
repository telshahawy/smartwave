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
    public class GetVisitReportQueryHandler : IQueryHandler<IGetVisitReportQuery, IGetVisitReportQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetVisitReportQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetVisitReportQueryResponse Read(IGetVisitReportQuery query)
        {

            IQueryable<VisitsHomePageView> dbQuery = _context.VisitsHomePageViews;
            IQueryable<UserView> userQuery = _context.UserViews;
            IQueryable<ChemistsView> chemistQuery = _context.ChemistsViews;
            IQueryable<CountryView> countryQuery = _context.CountryViews;
            IQueryable<GovernateView> govQuery = _context.GovernateViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            //var totalVisits = dbQuery;

            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            var totalVisits = dbQuery.Where(x => x.VisitDate >= query.VisitDateFrom && x.VisitDate <= query.VisitDateTo
               && (query.CountryOption == Guid.Empty || x.CountryId == query.CountryOption)
               && (query.GovernorateOption == Guid.Empty || x.GovernateId == query.GovernorateOption)
               && (query.AreaOption == Guid.Empty || x.GeoZoneId == query.AreaOption)
               && (query.ChemistOption == Guid.Empty || x.ChemistId == query.ChemistOption)
               //&& x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled
               );

            if (query.DelayedOption.ToLower() == "yes")
            {
                totalVisits = dbQuery.Where(p => (p.VisitDate.Date < DateTime.Now.Date && p.VisitStatusTypeId != (int)VisitStatusTypes.Done && p.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled) || p.VisitStatusCreationDate.Date > p.VisitDate.Date
                         || (p.VisitStatusCreationDate.Date == p.VisitDate.Date && p.VisitStatusCreationDate.TimeOfDay > p.EndTime));
            }
            else if (query.DelayedOption.ToLower() == "no")
            {
                totalVisits = dbQuery.Where(x => x.VisitDate.Date > DateTime.Now.Date || (x.VisitDate == DateTime.Now.Date && x.EndTime > DateTime.Now.TimeOfDay) ||
                        ((x.VisitStatusTypeId == (int)VisitStatusTypes.Done || x.VisitStatusTypeId == (int)VisitStatusTypes.Cancelled) && (x.VisitStatusCreationDate.Date < x.VisitDate.Date || (x.VisitStatusCreationDate.Date == x.VisitDate.Date && x.VisitStatusCreationDate.TimeOfDay < x.EndTime)))
                  );
            }

            //    var VisitsNo = totalVisits.Where(x => x.ChemistId == x.ChemistId).Count();
            //var DelayedVisitsNo = totalVisits.Where(x => x.VisitStatusTypeId == 2 || x.ChemistId == null || x.VisitStatusTypeId == 8).Count();
            //var NonDelayedVisitsNo = VisitsNo - DelayedVisitsNo;
            var country = query.CountryOption == Guid.Empty ? "All" : countryQuery.Where(x => x.CountryId == query.CountryOption).FirstOrDefault().CountryNameEn;
            var gov = query.GovernorateOption == Guid.Empty ? "All" : govQuery.Where(x => x.GovernateId == query.GovernorateOption).FirstOrDefault().GoverNameEn;
            var area = query.AreaOption == Guid.Empty ? "All" : geoQuery.Where(x => x.GeoZoneId == query.AreaOption).FirstOrDefault().NameEn;
            var chemist = query.ChemistOption == Guid.Empty ? "All" : chemistQuery.Where(x => x.ChemistId == query.ChemistOption).FirstOrDefault().Name;
            var userName = userQuery.Where(x => x.UserId == query.UserId).FirstOrDefault().Name;

            totalVisits = totalVisits.OrderBy(o => o.VisitDate);
            var visitNo = totalVisits.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                totalVisits = totalVisits.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new GetVisitReportQueryResponse
            {
                CountryOption = country,
                GovernorateOption = gov,
                AreaOption = area,
                ChemistOption = chemist,
                VisitDateFrom = query.VisitDateFrom.ToString("yyyy/MM/dd  hh:mm tt"),
                VisitDateTo = query.VisitDateTo.ToString("yyyy/MM/dd  hh:mm tt"),
                DelayedOption = query.DelayedOption,
                TotalVisitsNo = visitNo,
                PrintedBy = userName,
                PrintedDate = DateTime.UtcNow.ToString("yyyy/MM/dd  hh:mm tt"),
                VisitReports = totalVisits.Select(u => new VisitReportDto
                {
                    VisitDate = u.VisitDate.ToString("yyyy/MM/dd  hh:mm tt"),
                    VisitNo = u.VisitNo,
                    AreaNameAr = u.GeoZoneNameAr,
                    AreaNameEn = u.GeoZoneNameEn,
                    ChemistNameAr = u.ChemistName,
                    ChemistNameEn = u.ChemistName,
                    CountryNameAr = u.CountryNameAr,
                    CountryNameEn = u.CountryNameEn,
                    GovernorateNameAr = u.GoverNameAr,
                    GovernorateNameEn = u.GoverNameEn,
                    PhoneNumber = u.PhoneNumber,
                    Delayed = query.DelayedOption.ToLower() == "all" ? ((u.VisitDate.Date < DateTime.Now.Date && u.VisitStatusTypeId != (int)VisitStatusTypes.Done && u.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled) || u.VisitStatusCreationDate.Date > u.VisitDate.Date ||
                    (u.VisitStatusCreationDate.Date == u.VisitDate.Date && u.VisitStatusCreationDate.TimeOfDay > u.EndTime))
                    ? "Yes" : "No" : query.DelayedOption.ToLower() == "yes" ? "Yes" : "No" //u.VisitStatusTypeId == (int)VisitStatusTypes.Confirmed || u.VisitStatusTypeId == (int)VisitStatusTypes.Reject || u.ChemistId == null ? "Yes" : "No"

                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = visitNo,
                PageSize = query.PageSize
            } as IGetVisitReportQueryResponse;

        }
    }
}
