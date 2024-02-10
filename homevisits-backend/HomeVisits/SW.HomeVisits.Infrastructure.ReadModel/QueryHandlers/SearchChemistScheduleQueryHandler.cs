using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class SearchChemistScheduleQueryHandler : IQueryHandler<ISearchChemistScheduleQuery, ISearchChemistScheduleQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchChemistScheduleQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchChemistScheduleQueryResponse Read(ISearchChemistScheduleQuery query)
        {
            IQueryable<ChemistSchedulePlan> dbQuery = _context.ChemistSchedulePlanViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.ClientId == query.ClientId && x.ChemistId == query.ChemistId && x.ScheduleIsDeleted != true &&
                (query.StartDate == null || x.ScheuleStartDate.Date >= query.StartDate.Value.Date) &&
                (query.EndDate == null || x.ScheduleEndDate.Date <= query.EndDate.Value.Date) &&
                (query.AssignedGeoZoneId == null ||
                x.ChemistAssignedGeoZoneId == query.AssignedGeoZoneId.GetValueOrDefault())
                );

          var scheduleQuery = dbQuery.OrderByDescending(x => x.ScheuleStartDate).ToList().GroupBy(x => x.ScheuleStartDate);
          
            return new SearchChemistScheduleQueryResponse
            {
                Schedules = scheduleQuery.Select(x => new SearchChemistScheduleDto

                {
                    ChemistScheduleId = x.FirstOrDefault().ChemistScheduleId,
                    EndDate = x.FirstOrDefault().ScheduleEndDate,
                    GeoZoneName = x.FirstOrDefault().GeoZoneNameEn,
                    StartDate = x.FirstOrDefault().ScheuleStartDate
                }).ToList(),
            } as ISearchChemistScheduleQueryResponse;
        }
     
    }
}
