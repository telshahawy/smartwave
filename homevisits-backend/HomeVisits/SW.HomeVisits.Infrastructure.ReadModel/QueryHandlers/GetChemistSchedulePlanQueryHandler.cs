using System;
using Common.Logging;
using SW.Framework.Cqrs;
using System.Linq;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Application.Abstract.Dtos;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using SW.HomeVisits.Domain.Enums;
using System.Globalization;
using System.Data.Entity.Core.Common.CommandTrees;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetChemistSchedulePlanQueryHandler : IQueryHandler<IGetChemistSchedulePlanQuery, IGetChemistSchedulePlanQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistSchedulePlanQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistSchedulePlanQueryResponse Read(IGetChemistSchedulePlanQuery query)
        {
            IQueryable<ChemistSchedulePlan> dbQuery = _context.ChemistSchedulePlanViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(a => a.ClientId == query.ClientId && a.ChemistId == query.ChemistId
                && query.date >= a.ScheuleStartDate && query.date < a.ScheduleEndDate && a.AssignedZoneIsActive && 
                !a.AssignedZoneIsDeleted && a.ScheduleIsActive && !a.ScheduleIsDeleted && !a.ScheduleDaysIsDeleted);

                if (query.DayFilter)
                {
                    dbQuery = dbQuery.Where(a => a.Day == DayOfWeek(query.date, System.DayOfWeek.Sunday));
                }
            }

            return new GetChemistSchedulePlanQueryResponse()
            {
                ChemistSchedulePlans = dbQuery.Select(a => new ChemistSchedulePlanDto
                {
                    ChemistScheduleDayId = a.ChemistScheduleDayId,
                    Day = a.Day,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime
                }).ToList()
            } as IGetChemistSchedulePlanQueryResponse;
        }
        private int DayOfWeek(DateTime value, DayOfWeek firstDayOfWeek)
        {
            var idx = 7 + (int)value.DayOfWeek - (int)firstDayOfWeek;
            if (idx > 6) // week ends at 6, because Enum.DayOfWeek is zero-based
            {
                idx -= 7;
            }
            return idx + 1;
        }
    }
}
