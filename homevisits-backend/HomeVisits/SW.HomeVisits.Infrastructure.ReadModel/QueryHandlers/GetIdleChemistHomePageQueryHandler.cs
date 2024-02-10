using SW.Framework.Cqrs;
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
    public class GetIdleChemistHomePageQueryHandler : IQueryHandler<IGetIdleChemistHomePageQuery, IGetIdleChemistHomePageQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetIdleChemistHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetIdleChemistHomePageQueryResponse Read(IGetIdleChemistHomePageQuery query)
        {
            //IQueryable<ChemistScheduleHomePageView> chemistScheduleQuery = _context.ChemistScheduleHomePageViews;
            IQueryable<VisitsHomePageView> chemistScheduleQuery = _context.VisitsHomePageViews;
            var idleChemist = chemistScheduleQuery;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            if (query.GeoZoneId != Guid.Empty)
            {
                idleChemist = chemistScheduleQuery.Where(x => x.VisitDate.Date == DateTime.Today && x.GeoZoneId == query.GeoZoneId);
               
            }
            idleChemist = idleChemist.Where(x => x.VisitDate == DateTime.Now && x.StartTime! <= (DateTime.Now.AddMinutes(30).TimeOfDay) && DateTime.Now.AddMinutes(-30).TimeOfDay! <= x.EndTime
                   && (query.GeoZoneId == Guid.Empty || x.GeoZoneId == query.GeoZoneId) && (x.ChemistId != null)).OrderBy(o => o.VisitDate);

            return new GetIdleChemistHomePageQueryResponse
            {
                IdleChemistNames = idleChemist.Select(x => x.ChemistName).ToList()

            } as IGetIdleChemistHomePageQueryResponse;
        }
    }
}
