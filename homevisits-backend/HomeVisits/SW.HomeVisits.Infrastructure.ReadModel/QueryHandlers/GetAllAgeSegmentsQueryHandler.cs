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
    internal class GetAllAgeSegmentsQueryHandler : IQueryHandler<IGetAllAgeSegmentsQuery, IGetAllAgeSegmentsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetAllAgeSegmentsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetAllAgeSegmentsQueryResponse Read(IGetAllAgeSegmentsQuery query)
        {
            IQueryable<AgeSegmentsView> dbQuery = _context.AgeSegmentsViews;

            if (query != null)
            {
                dbQuery = dbQuery.Where(a => a.ClientId == query.ClientId && a.IsActive == true && a.IsDeleted == false);
            }

            return new GetAllAgeSegmentsQueryResponse()
            {
                AgeSegments = dbQuery.Select(a => new AgeSegmentsDto
                {
                    AgeSegmentId = a.AgeSegmentId,
                    Name = a.Name,
                    AgeFromDay = a.AgeFromDay,
                    AgeFromMonth = a.AgeFromMonth,
                    AgeFromYear = a.AgeFromYear,
                    AgeToDay = a.AgeToDay,
                    AgeToMonth = a.AgeToMonth,
                    AgeToYear = a.AgeToYear,
                    NeedExpert = a.NeedExpert
                }).ToList()
            } as IGetAllAgeSegmentsQueryResponse;
        }
    }
}
