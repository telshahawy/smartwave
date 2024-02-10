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
    public class GetChemistScheduleForEditQueryHandler : IQueryHandler<IGetChemistScheduleForEditQuery, IGetChemistScheduleForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetChemistScheduleForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetChemistScheduleForEditQueryResponse Read(IGetChemistScheduleForEditQuery query)
        {
            IQueryable<ChemistSchedulePlan> dbQuery = _context.ChemistSchedulePlanViews;
            IQueryable<ChemistsView> chQuery = _context.ChemistsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.ClientId == query.ClientId && x.ScheduleIsDeleted != true && x.ChemistScheduleId == query.ChemistScheduleId);

            var scheduleQuery = dbQuery.OrderBy(x => x.ScheuleStartDate).ToList().GroupBy(x => x.ChemistScheduleId);
            var schdule = scheduleQuery.FirstOrDefault();
            var chemistId = schdule.FirstOrDefault().ChemistId;
            if (schdule == null)
            {
                throw new Exception("No Schedule Found");
            }
            return new GetChemistScheduleForEditQueryResponse
            {
                Schedule = new ChemistScheduleForEditDto
                {
                    ChemistScheduleId = schdule.Key,
                    ChemistId = chemistId,
                    ChemistName=chQuery?.Where(x=>x.ChemistId== chemistId).FirstOrDefault().Name,
                    AssignedChemistGeoZoneId = schdule.FirstOrDefault().ChemistAssignedGeoZoneId,
                    EndDate = schdule.FirstOrDefault().ScheduleEndDate,
                    StartDate = schdule.FirstOrDefault().ScheuleStartDate,
                    StartLatitude=schdule.FirstOrDefault().StartLatitude,
                    StartLangitude=schdule.FirstOrDefault().StartLangitude,
                    FriEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Fri).SingleOrDefault()?.EndTime,
                    FriStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Fri).SingleOrDefault()?.StartTime,
                    SatEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Sat).SingleOrDefault()?.EndTime,
                    SatStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Sat).SingleOrDefault()?.StartTime,
                    SunEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Sun).SingleOrDefault()?.EndTime,
                    SunStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Sun).SingleOrDefault()?.StartTime,
                    MonEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Mon).SingleOrDefault()?.EndTime,
                    MonStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Mon).SingleOrDefault()?.StartTime,
                    TueEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Tue).SingleOrDefault()?.EndTime,
                    TueStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Tue).SingleOrDefault()?.StartTime,
                    WedEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Wed).SingleOrDefault()?.EndTime,
                    WedStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Wed).SingleOrDefault()?.StartTime,
                    ThuEnd = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Thu).SingleOrDefault()?.EndTime,
                    ThuStart = schdule.Where(d => d.Day == (int)SW.HomeVisits.Domain.Enums.Days.Thu).SingleOrDefault()?.StartTime,
                }
            } as IGetChemistScheduleForEditQueryResponse;
        }
    }
}
