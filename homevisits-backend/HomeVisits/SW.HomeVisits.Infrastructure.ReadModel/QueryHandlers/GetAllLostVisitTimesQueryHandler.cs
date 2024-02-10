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

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    internal class GetAllLostVisitTimesQueryHandler : IQueryHandler<IGetAllLostVisitTimesQuery, IGetAllLostVisitTimesQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetAllLostVisitTimesQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetAllLostVisitTimesQueryResponse Read(IGetAllLostVisitTimesQuery query)
        {
            IQueryable<LostVisitTimesView> dbQuery = _context.LostVisitTimeViews;

            return new GetAllLostVisitTimesQueryResponse()
            {
                lostVisitTimes = dbQuery.Select(l => new LostVisitTimesDto
                {
                    LostVisitTimeId = l.LostVisitTimeId,
                    CreatedBy = l.CreatedBy,
                    VisitId = l.VisitId,
                    LostTime = l.LostTime,
                    CreatedOn = l.CreatedOn,
                    VisitNo = l.VisitNo,
                    VisitCode=l.VisitCode
                }).ToList()
            } as IGetAllLostVisitTimesQueryResponse;
        }
    }
}
