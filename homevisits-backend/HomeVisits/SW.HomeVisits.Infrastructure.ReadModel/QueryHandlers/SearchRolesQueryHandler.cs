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
    public class SearchRolesQueryHandler : IQueryHandler<ISearchRolesQuery, ISearchRolesQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchRolesQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }
        public ISearchRolesQueryResponse Read(ISearchRolesQuery query)
        {
            IQueryable<RolesView> dbQuery = _context.RolesViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }
            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId &&
                (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.Name) || x.NameAr == query.Name) &&
                (query.IsActive == null || x.IsActive == query.IsActive)
                ).OrderBy(x=>x.Code);
            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }
            return new SearchRolesQueryResponse
            {
                Roles = dbQuery.Select(x => new SearchRoleDto
                {
                    Code = x.Code,
                    IsActive = x.IsActive,
                    Name = x.NameAr,
                    RoleId = x.RoleId
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchRolesQueryResponse;
        }
    }
}
