using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;
using SW.HomeVisits.Domain.Enums;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class SearchClientUserQueryHandler : IQueryHandler<ISearchClientUserQuery, ISearchClientUserQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public SearchClientUserQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public ISearchClientUserQueryResponse Read(ISearchClientUserQuery query)
        {
            IQueryable<UserView> dbQuery = _context.UserViews;
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            dbQuery = dbQuery.Where(x => x.IsDeleted != true && x.ClientId == query.ClientId && x.UserType == (int)UserTypes.ClientAdmin &&
            (query.Code == null || x.Code == query.Code) &&
                (string.IsNullOrWhiteSpace(query.Name) || x.Name == query.Name) &&
                (string.IsNullOrWhiteSpace(query.PhoneNumber) || x.PhoneNumber == query.PhoneNumber) &&
                (query.IsActive == null || x.IsActive == query.IsActive)
                ).OrderBy(p => p.Code);

            var totalCount = dbQuery.Count();
            if (query.CurrentPageIndex != null && query.CurrentPageIndex != 0 && query.PageSize != null && query.PageSize != 0)
            {
                int skipRows = (query.CurrentPageIndex.Value - 1) * query.PageSize.Value;
                dbQuery = dbQuery.Skip(skipRows).Take(query.PageSize.Value);
            }

            return new SearchClientUserQueryResponse()
            {
                Users = dbQuery.Select(x => new SearchClientUserDto
                {
                    Code = x.Code,
                    IsActive = x.IsActive,
                    PhoneNumber = x.PhoneNumber,
                    Name = x.Name,
                    UserId = x.UserId
                }).ToList(),
                CurrentPageIndex = query.CurrentPageIndex,
                TotalCount = totalCount,
                PageSize = query.PageSize
            } as ISearchClientUserQueryResponse;
        }
    }
}
