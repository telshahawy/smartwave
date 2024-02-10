using System;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetUserSystemPageWithPermissionsTreeQueryHandler : IQueryHandler<IGetUserSystemPageWithPermissionsTreeQuery, IGetUserSystemPageWithPermissionsTreeQueryResponse>
    {
        private readonly ILog _log;
        private readonly HomeVisitsReadModelContext _context;

        public GetUserSystemPageWithPermissionsTreeQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _log = log;
            _context = context;
        }

        public IGetUserSystemPageWithPermissionsTreeQueryResponse Read(IGetUserSystemPageWithPermissionsTreeQuery query)
        {
            IQueryable<UserPermissionView> dbQuery = _context.UserPermissionViews;

            var systemPagesWithPermissions = dbQuery.Where(p => p.UserId == query.UserId).ToList().GroupBy(x => x.SystemPageId);

            var systemPageTreeDtos = systemPagesWithPermissions.Select(x => new SystemPageTreeDto
            {
                Id = x.Key,
                Name = query.CultureName == CultureNames.ar ? x.First().SystemPageNameAr : x.First().SystemPageNameEn,
                Code = x.First().SystemPageCode,
                HasURL = x.First().SystemPageHasURL,
                ParentId = x.First().SystemPageParentId,
                IsDisplayInMenue = x.First().SystemPageIsDisplayInMenue,
                Position = x.First().SystemPagePosition,
                Permissions = x.OrderBy(p => p.PermissionPosition).Select(s => new PermissionTreeDto
                {
                    SystemPagePermissionId = s.SystemPagePermissionId,
                    PermissionId = s.PermissionId,
                    PermissionName = query.CultureName == CultureNames.ar ? s.PermissionNameAr : s.PermissionNameEn,
                    PermissionCode = s.PermissionCode,
                    PermissionPosition = s?.PermissionPosition ?? 1
                }).ToList(),
                SubChild = x.OrderBy(p=>p.PermissionPosition).Select(s=>new PermissionTree
                {
                    Id = s.PermissionId,
                    Name = query.CultureName == CultureNames.ar ? s.PermissionNameAr : s.PermissionNameEn
                }).ToList()
            }).ToList();


            var parentPagesId = systemPageTreeDtos.Where(p => p.ParentId.HasValue).Select(p => p.ParentId).Distinct().ToList();
            var parentSystemPages = _context.SystemPagesViews.Where(p => parentPagesId.Contains(p.SystemPageId)).ToList();
            systemPageTreeDtos.AddRange(parentSystemPages.Where(p => !systemPageTreeDtos.Any(m => m.Id == p.SystemPageId)).Select(x => new SystemPageTreeDto
            {
                Id = x.SystemPageId,
                Name = query.CultureName == CultureNames.ar ? x.NameAr : x.NameEn,
                Code = x.Code,
                HasURL = x.HasURL,
                ParentId = x.ParentId,
                IsDisplayInMenue = x.IsDisplayInMenue,
                Position = x.Position
            }));

            return new GetUserSystemPageWithPermissionsTreeQueryResponse()
            {
                SystemPages = systemPageTreeDtos
            } as IGetUserSystemPageWithPermissionsTreeQueryResponse;
        }
    }
}
