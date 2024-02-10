using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetSystemPermissionQueryHandler : IQueryHandler<IGetSystemPagePermissionQuery, IGetSystemPagePermissionQueryResponse>
    {
        private readonly ILog _log;
        private readonly HomeVisitsReadModelContext _context;

        public GetSystemPermissionQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _log = log;
            _context = context;
        }

        public IGetSystemPagePermissionQueryResponse Read(IGetSystemPagePermissionQuery query)
        {
            IQueryable<SystemPagesPermissionsView> dbQuery = _context.SystemPagesPermissionsView;
            if (query == null)
                throw new NullReferenceException(nameof(query));

            if (query.PermissionCode.HasValue)
                dbQuery = dbQuery.Where(p => p.PermissionCode == query.PermissionCode.Value);

            if (!string.IsNullOrEmpty(query.SystemPageCode))
                dbQuery = dbQuery.Where(p => p.SystemPageCode.Trim().ToLower() == query.SystemPageCode.Trim().ToLower());

            if (query.PermissionId.HasValue)
                dbQuery = dbQuery.Where(p => p.PermissionId == query.PermissionId.Value);

            if (query.SystemPagePermissionId.HasValue)
                dbQuery = dbQuery.Where(p => p.SystemPagePermissionId == query.SystemPagePermissionId);

            if (query.SystemPageId.HasValue)
                dbQuery = dbQuery.Where(p => p.SystemPageId == query.SystemPageId);

            var permissions = dbQuery.ToList();
            return new GetSystemPagePermissionQueryResponse
            {
                SystemPagePermissionDtos = permissions.Select(p => new SystemPagePermissionDto
                {
                    PermissionId = p.PermissionId,
                    HasURL = p.HasURL,
                    IsDisplayInMenue = p.IsDisplayInMenue,
                    ParentId = p.ParentId,
                    PermissionCode = p.PermissionCode,
                    PermissionIsActive = p?.PermissionIsActive ?? true,
                    PermissionName = query.CultureName == CultureNames.ar ? p.PermissionNameAr : p.PermissonNameEn,
                    PermissionPosition = p?.PermissionPosition ?? 1,
                    SystemPageCode = p.SystemPageCode,
                    SystemPageId = p.SystemPageId,
                    SystemPageName = query.CultureName == CultureNames.ar ? p.SystemPageNameAr : p.SystemPageNameEn,
                    SystemPagePermissionId = p.SystemPagePermissionId,
                    SystemPagePosition = p.SystemPagePosition
                }).ToList()
            } as IGetSystemPagePermissionQueryResponse;
        }
    }
}

