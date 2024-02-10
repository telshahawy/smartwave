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
    public class GetAgeSegmentForEditQueryHandler : IQueryHandler<IGetAgeSegmentForEditQuery, IGetAgeSegmentForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetAgeSegmentForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetAgeSegmentForEditQueryResponse Read(IGetAgeSegmentForEditQuery query)
        {
            IQueryable<AgeSegmentsView> dbQuery = _context.AgeSegmentsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var ageSegment = dbQuery.SingleOrDefault(x => x.AgeSegmentId == query.AgeSegmentId);
            if (ageSegment == null)
            {
                throw new Exception("AgeSegment not found");
            }
            return new GetAgeSegmentForEditQueryResponse
            {
                AgeSegment = new AgeSegmentsDto
                {
                    AgeSegmentId = ageSegment.AgeSegmentId,
                    Code = ageSegment.Code,
                    Name = ageSegment.Name,
                    AgeFromDay = ageSegment.AgeFromDay,
                    AgeFromMonth = ageSegment.AgeFromMonth,
                    AgeFromYear = ageSegment.AgeFromYear,
                    AgeFromInclusive = ageSegment.AgeFromInclusive,
                    AgeToDay = ageSegment.AgeToDay,
                    AgeToMonth = ageSegment.AgeToMonth,
                    AgeToYear = ageSegment.AgeToYear,
                    AgeToInclusive = ageSegment.AgeToInclusive,
                    IsActive = ageSegment.IsActive,
                    IsDeleted = ageSegment.IsDeleted,
                    NeedExpert = ageSegment.NeedExpert
                }
            
            } as IGetAgeSegmentForEditQueryResponse;
        }
    }
}

