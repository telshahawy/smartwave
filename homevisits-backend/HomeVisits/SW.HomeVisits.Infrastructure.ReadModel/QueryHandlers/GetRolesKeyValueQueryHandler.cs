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
    public class GetRolesKeyValueQueryHandler : IQueryHandler<IGetRolesKeyValueQuery, IGetRolesKeyValueQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetRolesKeyValueQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetRolesKeyValueQueryResponse Read(IGetRolesKeyValueQuery query)
        {
            IQueryable<RolesView> dbQuery = _context.RolesViews;
            if (query != null)
            {
                dbQuery = dbQuery.Where(x => x.ClientId == query.ClientId);
            }

            return new GetRolesKeyValueQueryResponse()
            {
                Roles = dbQuery.Select(x=> new RoleKeyValueDto
                {
                    Name = x.NameAr,
                    RoleId = x.RoleId
                }).ToList()
            } as IGetRolesKeyValueQueryResponse;
        }
    }
}

