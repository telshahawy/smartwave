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
    public class GetNonDetailedVisitReportQueryHandler : IQueryHandler<IGetVisitReportQuery, IGetNonDetailedVisitReportQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetNonDetailedVisitReportQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }

        public IGetNonDetailedVisitReportQueryResponse Read(IGetVisitReportQuery query)
        {
            IQueryable<VisitsHomePageView> dbQuery = _context.VisitsHomePageViews;
            IQueryable<ChemistsView> chemistQuery = _context.ChemistsViews;
            IQueryable<UserView> userQuery = _context.UserViews;
            IQueryable<CountryView> countryQuery = _context.CountryViews;
            IQueryable<GovernateView> govQuery = _context.GovernateViews;
            IQueryable<GeoZoneView> geoQuery = _context.GeoZoneView;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var totalVisits = dbQuery.Where(x => x.ChemistId.HasValue && x.VisitDate >= query.VisitDateFrom && x.VisitDate <= query.VisitDateTo
                 && (query.CountryOption == Guid.Empty || x.CountryId == query.CountryOption)
                 && (query.GovernorateOption == Guid.Empty || x.GovernateId == query.GovernorateOption)
                 && (query.AreaOption == Guid.Empty || x.GeoZoneId == query.AreaOption)
                 && (query.ChemistOption == Guid.Empty || x.ChemistId == query.ChemistOption)
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

            var country = query.CountryOption == Guid.Empty ? "All" : countryQuery.Where(x => x.CountryId == query.CountryOption).FirstOrDefault().CountryNameEn;
            var gov = query.GovernorateOption == Guid.Empty ? "All" : govQuery.Where(x => x.GovernateId == query.GovernorateOption).FirstOrDefault().GoverNameEn;
            var area = query.AreaOption == Guid.Empty ? "All" : geoQuery.Where(x => x.GeoZoneId == query.AreaOption).FirstOrDefault().NameEn;
            var chemist = query.ChemistOption == Guid.Empty ? "All" : chemistQuery.Where(x => x.ChemistId == query.ChemistOption).FirstOrDefault().Name;
            var userName = userQuery.Where(x => x.UserId == query.UserId).FirstOrDefault().Name;

            var totalVisitsByChemist = totalVisits.ToList().GroupBy(p => p.ChemistId);
            var visitNo = totalVisits.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                totalVisits = totalVisits.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new GetNonDetailedVisitReportQueryResponse()
            {
                TotalVisitsNo = visitNo,
                CountryOption = country,
                GovernorateOption = gov,
                AreaOption = area,
                ChemistOption = chemist,
                VisitDateFrom = query.VisitDateFrom.ToString("yyyy/MM/dd  hh:mm tt"),
                VisitDateTo = query.VisitDateTo.ToString("yyyy/MM/dd  hh:mm tt"),
                PrintedBy = userName,
                DelayedOption = query.DelayedOption,
                PrintedDate = DateTime.UtcNow.ToString("yyyy/MM/dd  hh:mm tt"),

                NonDetailedVisitReports = totalVisitsByChemist.Select(x => new NonDetailedVisitReportDto
                {
                    ChemistNameAr = x.First().ChemistName,
                    ChemistNameEn = x.First().ChemistName,
                    VisitsCount = x.Count(),
                    DelayedVisitsCount = query.DelayedOption.ToLower() == "all" ? x.Count(m => (m.VisitDate.Date < DateTime.Now.Date && m.VisitStatusTypeId != (int)VisitStatusTypes.Done && m.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled) || m.VisitStatusCreationDate.Date > m.VisitDate.Date ||
                        (m.VisitStatusCreationDate.Date == m.VisitDate.Date && m.VisitStatusCreationDate.TimeOfDay > m.EndTime)) : query.DelayedOption.ToLower() == "yes" ? x.Count() : 0,//totalVisits.Where(y => y.ChemistId == x.ChemistId && x.VisitStatusTypeId == (int)VisitStatusTypes.Confirmed || x.ChemistId == null || x.VisitStatusTypeId == (int)VisitStatusTypes.Reject).Count(),//8alt
                    NonDelayedVisitsCount = query.DelayedOption.ToLower() == "all" ? x.Count(m => m.VisitDate.Date > DateTime.Now.Date || (m.VisitDate == DateTime.Now.Date && m.EndTime > DateTime.Now.TimeOfDay) ||
                                  ((m.VisitStatusTypeId == (int)VisitStatusTypes.Done || m.VisitStatusTypeId == (int)VisitStatusTypes.Cancelled) && (m.VisitStatusCreationDate.Date < m.VisitDate.Date || (m.VisitStatusCreationDate.Date == m.VisitDate.Date && m.VisitStatusCreationDate.TimeOfDay < m.EndTime)))) : query.DelayedOption.ToLower() == "no" ? x.Count() : 0,//(totalVisits.Where(y => y.ChemistId == x.ChemistId).Count()) - (totalVisits.Where(y => y.ChemistId == x.ChemistId && x.VisitStatusTypeId == (int)VisitStatusTypes.Confirmed || x.ChemistId == null || x.VisitStatusTypeId == (int)VisitStatusTypes.Reject).Count())
                }).OrderBy(p => p.ChemistNameEn).Distinct().ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = visitNo,
                PageSize = query.PageSize
            } as IGetNonDetailedVisitReportQueryResponse;
        }

    }
}
