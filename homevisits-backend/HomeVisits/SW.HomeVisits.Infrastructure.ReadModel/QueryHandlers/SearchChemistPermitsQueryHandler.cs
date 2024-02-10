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
    public class SearchChemistPermitsQueryHandler : IQueryHandler<ISearchChemistPermitsQuery, ISearchChemistPermitsQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchChemistPermitsQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchChemistPermitsQueryResponse Read(ISearchChemistPermitsQuery query)
        {
            IQueryable<ChemistPermitsView> dbQuery = _context.ChemistPermitsViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId && x.ChemistId == query.ChemistId &&
                (query.PermitDate == null || x.PermitDate == query.PermitDate));

            return new SearchChemistPermitsQueryResponse()
            {
                Permits = dbQuery.Select(x => new SearchChemistPermitDto
                {
                    PermitDate = x.PermitDate,
                    ChemistPermitId = x.ChemistPermitId,
                    EndTime = x.EndTime.ToString(),
                    StartTime = x.StartTime.ToString()
                }).ToList(),
            } as ISearchChemistPermitsQueryResponse;
        }
    }
}
