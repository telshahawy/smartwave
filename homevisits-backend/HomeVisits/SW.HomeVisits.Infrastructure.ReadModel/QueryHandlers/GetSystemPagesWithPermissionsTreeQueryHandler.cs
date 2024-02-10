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
    public class GetSystemPagesWithPermissionsTreeQueryHandler : IQueryHandler<IGetSystemPagesWithPermissionsTreeQuery, IGetSystemPagesWithPermissionsTreeQueryResponse>
    {
        private readonly ILog _log;
        private readonly HomeVisitsReadModelContext _context;

        public GetSystemPagesWithPermissionsTreeQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _log = log;
            _context = context;
        }

        public IGetSystemPagesWithPermissionsTreeQueryResponse Read(IGetSystemPagesWithPermissionsTreeQuery query)
        {
            IQueryable<SystemPagesWithPermissionsView> dbQuery = _context.SystemPagesWithPermissionsViews;

            var systemPagesWithoutPermissions = dbQuery.Where(p => !p.PermissionId.HasValue).ToList().GroupBy(x => x.SystemPageId);
            var systemPagesWithPermissions = dbQuery.Where(p => p.PermissionId.HasValue).ToList().GroupBy(x => x.SystemPageId);

            var result = systemPagesWithPermissions.Select(x => new SystemPageTreeDto
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
                    SystemPagePermissionId = s.SystemPagePermissionId.Value,
                    PermissionId = s.PermissionId.Value,
                    PermissionName = query.CultureName == CultureNames.ar ? s.PermissionNameAr : s.PermissonNameEn,
                    PermissionCode = s.PermissionCode.Value,
                    PermissionPosition = s?.PermissionPosition ?? 1
                }).ToList(),
                SubChild = x.OrderBy(p => p.PermissionPosition).Select(s => new PermissionTree
                {
                    Id = s.SystemPagePermissionId.Value,
                    Name = query.CultureName == CultureNames.ar ? s.PermissionNameAr : s.PermissonNameEn
                }).ToList()
            }).ToList();

            result.AddRange(systemPagesWithoutPermissions.Select(x => new SystemPageTreeDto
            {
                Id = x.Key,
                Name = query.CultureName == CultureNames.ar ? x.First().SystemPageNameAr : x.First().SystemPageNameEn,
                Code = x.First().SystemPageCode,
                HasURL = x.First().SystemPageHasURL,
                ParentId = x.First().SystemPageParentId,
                IsDisplayInMenue = x.First().SystemPageIsDisplayInMenue,
                Position = x.First().SystemPagePosition
            }));

            return new GetSystemPagesWithPermissionsTreeQueryResponse()
            {
                SystemPages = result
            } as IGetSystemPagesWithPermissionsTreeQueryResponse;
        }
    }
}
