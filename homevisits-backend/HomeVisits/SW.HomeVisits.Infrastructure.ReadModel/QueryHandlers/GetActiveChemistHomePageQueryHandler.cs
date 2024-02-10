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
    public class GetActiveChemistHomePageQueryHandler : IQueryHandler<IGetActiveChemistHomePageQuery, IGetActiveChemistHomePageQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        public GetActiveChemistHomePageQueryHandler(HomeVisitsReadModelContext context)
        {
            _context = context;
        }
        public IGetActiveChemistHomePageQueryResponse Read(IGetActiveChemistHomePageQuery query)
        {
            //IQueryable<ChemistScheduleHomePageView> chemistScheduleQuery = _context.ChemistScheduleHomePageViews;
            IQueryable<VisitsHomePageView> dbQuery = _context.VisitsHomePageViews;
            var activeChemist = dbQuery;


            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            if (query.GeoZoneId != Guid.Empty)
            {
                activeChemist = dbQuery.Where(x => x.VisitDate.Date == DateTime.Today && x.GeoZoneId == query.GeoZoneId);


            }
            activeChemist = activeChemist.Where(x => x.StartTime <= (DateTime.Now.AddMinutes(30).TimeOfDay) && DateTime.Now.AddMinutes(-30).TimeOfDay <= x.EndTime
                && (query.GeoZoneId == Guid.Empty || x.GeoZoneId == query.GeoZoneId) && (x.ChemistId != null)).OrderBy(o => o.ChemistName);
          
            return new GetActiveChemistHomePageQueryResponse
            {
               ActiveChemistNames=activeChemist.Select(x=>x.ChemistName).ToList()

            } as IGetActiveChemistHomePageQueryResponse;
        }
    }
}
