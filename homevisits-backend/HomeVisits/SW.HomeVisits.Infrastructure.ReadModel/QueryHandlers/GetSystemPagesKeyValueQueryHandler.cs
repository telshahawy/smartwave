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
    public class GetSystemPagesKeyValueQueryHandler : IQueryHandler<IGetSystemPageKeyValueQuery, IGetSystemPagesKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetSystemPagesKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetSystemPagesKeyValueQueryResponse Read(IGetSystemPageKeyValueQuery query)
        {
            IQueryable<SystemPagesWithPermissionsView> dbQuery = _context.SystemPagesWithPermissionsViews.Where(p => p.SystemPageHasURL);

            var systemPages = dbQuery.ToList().GroupBy(x => x.SystemPageId);
            return new GetSystemPagesKeyValueQueryResponse()
            {
                SystemPages = systemPages.Select(x => new SystemPagekeyValueDto
                {
                    Name = x.First().SystemPageNameEn,
                    Id = x.Key
                }).ToList()
            } as IGetSystemPagesKeyValueQueryResponse;
        }
    }
}

