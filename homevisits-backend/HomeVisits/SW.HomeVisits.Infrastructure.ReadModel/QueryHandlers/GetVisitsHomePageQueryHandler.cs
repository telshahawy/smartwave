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
    class GetVisitsHomePageQueryHandler : IQueryHandler<IGetVisitsHomePageQuery, IGetVisitsHomePageQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetVisitsHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetVisitsHomePageQueryResponse Read(IGetVisitsHomePageQuery query)
        {
            IQueryable<VisitsHomePageView> dbQuery = _context.VisitsHomePageViews;
            IQueryable<AllChemistHomePageView> chemistDdbQuery = _context.AllChemistHomePageViews;

            if (query == null)
                throw new NullReferenceException(nameof(query));


            var HomePageVisits = dbQuery.Where(x => (query.GeoZoneId == Guid.Empty || x.GeoZoneId == query.GeoZoneId) && x.VisitDate.Date == DateTime.Today.Date);

            //var otherVisits = dbQuery;
            //if (query.GeoZoneId != Guid.Empty)
            //{
            //    //otherVisits = dbQuery.Where(x => x.GeoZoneId == query.GeoZoneId);
            //    HomePageVisits = dbQuery.Where(x => x.VisitDate.Date == DateTime.Today.Date && x.GeoZoneId == query.GeoZoneId);
            //}

            var chemistQuery = chemistDdbQuery.ToList();
            //a.Start<b.end&&b.start<a.end//...
            var activeChemists = HomePageVisits.Where(x => (query.GeoZoneId == Guid.Empty || x.GeoZoneId == query.GeoZoneId) && x.ChemistId != null
            && ((x.StartTime >= DateTime.Now.AddMinutes(-30).TimeOfDay && x.StartTime <= DateTime.Now.AddMinutes(30).TimeOfDay) ||
                (x.StartTime <= DateTime.Now.AddMinutes(-30).TimeOfDay && x.EndTime <= DateTime.Now.AddMinutes(30).TimeOfDay) ||
                (x.StartTime <= DateTime.Now.AddMinutes(-30).TimeOfDay && x.EndTime > DateTime.Now.AddMinutes(30).TimeOfDay))).ToList();
            //HomePageVisits.Where(x => x.StartTime <= (DateTime.Now.AddMinutes(30).TimeOfDay) && DateTime.Now.AddMinutes(-30).TimeOfDay <= x.EndTime
            //&& (query.GeoZoneId == Guid.Empty || x.GeoZoneId == query.GeoZoneId) && (x.ChemistId != null));

            var idleChemist = chemistQuery.GroupJoin(activeChemists,
                      chemists => chemists.ChemistId, activeChemist => activeChemist.ChemistId,
                      (x, y) => new { chemists = x, activeChemist = y })
                      .SelectMany(x => x.activeChemist.DefaultIfEmpty(),
                            (x, y) => new { Chemists = x.chemists, ActiveChemist = y }).ToList();
            //HomePageVisits.Where(x => x.VisitDate == DateTime.Now && x.StartTime! <= (DateTime.Now.AddMinutes(30).TimeOfDay) && DateTime.Now.AddMinutes(-30).TimeOfDay! <= x.EndTime
            //&& (query.GeoZoneId == Guid.Empty || x.GeoZoneId == query.GeoZoneId) && (x.ChemistId != null));



            var pendingVisits = HomePageVisits.Count(x => x.ChemistId == null || x.VisitStatusTypeId == (int)VisitStatusTypes.Reject);
            var canceledVisits = HomePageVisits.Count(x => x.VisitStatusTypeId == (int)VisitStatusTypes.Cancelled);
            var newVisits = HomePageVisits.Count(x => x.VisitStatusTypeId == (int)VisitStatusTypes.New);
            var doneVisits = HomePageVisits.Count(x => x.VisitStatusTypeId == (int)VisitStatusTypes.Done);
            var rejectedVisits = HomePageVisits.Count(x => x.VisitStatusTypeId == (int)VisitStatusTypes.Reject);
            var confirmedVisits = HomePageVisits.Count(x => x.VisitStatusTypeId == (int)VisitStatusTypes.Confirmed);
            var reassignedVisits = HomePageVisits.Count(x => x.VisitActionTypeId == (int)VisitActionTypes.ReassignChemist);
            var secondVisits = HomePageVisits.Count(x => x.VisitActionTypeId == (int)VisitActionTypes.RequestSecondVisit || x.VisitActionTypeId == (int)VisitActionTypes.AcceptAndRequestSecondVisit);
            var delayedVisits = HomePageVisits.Count(x => (x.EndTime < DateTime.Now.TimeOfDay && x.VisitStatusTypeId != (int)VisitStatusTypes.Done && x.VisitStatusTypeId != (int)VisitStatusTypes.Cancelled) || (x.VisitStatusTypeId == (int)VisitStatusTypes.Done && x.VisitStatusCreationDate.TimeOfDay < x.EndTime));

            return new GetVisitsHomePageQueryResponse
            {
                //Visit statistics///
                CanceledVisitsNo = canceledVisits,
                DoneVisitsNo = doneVisits,
                RejectedVisitsNo = rejectedVisits,
                AllVisitsNo = pendingVisits + confirmedVisits + doneVisits + canceledVisits + rejectedVisits + reassignedVisits/* + secondVisits*/,
                DelayedVisitsNo = delayedVisits,//otherVisits.Count(x => x.VisitDate < DateTime.Now && x.VisitStatusTypeId == (int)VisitStatusTypes.Confirmed) + pendingVisits,//pending or confirmed
                PendingVisitsNo = pendingVisits,
                ConfirmedVisitsNo = confirmedVisits,
                ReassignedVisitsNo = reassignedVisits,
                //Chemist statistics///
                AllChemistNo = chemistQuery.Select(p => p.ChemistId).Distinct().Count(),
                ActiveChemistNo = activeChemists.Select(p => p.ChemistId).Distinct().Count(),
                IdleChemistNo = idleChemist.Where(p => p.ActiveChemist == null && p.Chemists != null).Select(p => p.Chemists.ChemistId).Distinct().Count()
            } as IGetVisitsHomePageQueryResponse;
        }
    }
}
