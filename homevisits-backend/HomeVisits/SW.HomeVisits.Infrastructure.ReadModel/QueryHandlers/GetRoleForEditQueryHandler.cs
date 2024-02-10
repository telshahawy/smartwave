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
    public class GetRoleForEditQueryHandler : IQueryHandler<IGetRoleForEditQuery, IGetRoleForEditQueryResponse>
    {
        private readonly HomeVisitsReadModelContext _context;
        private readonly ILog _log;

        public GetRoleForEditQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public IGetRoleForEditQueryResponse Read(IGetRoleForEditQuery query)
        {
            IQueryable<RolesView> dbQuery = _context.RolesViews;
            IQueryable<RolesPermissionView> RolePermissionDbQuery = _context.RolesPermissionViews.Where(x => x.RoleId == query.RoleId && !x.PermissionIsDeleted);
            IQueryable<RolesGeoZonesView> RoleGeoZonesDbQuery = _context.RolesGeoZonesViews.Where(x => x.RoleId == query.RoleId);
            if (query == null)
            {
                throw new NullReferenceException(nameof(query));
            }

            var role = dbQuery.SingleOrDefault(x => x.RoleId == query.RoleId);
            if (role == null)
            {
                throw new Exception("Role not found");
            }
            return new GetRoleForEditQueryResponse
            {
                Role = new RoleDto
                {
                    RoleId = role.RoleId,
                    ClientId = role.ClientId,
                    Code = role.Code,
                    CreatedAt = role.CreatedAt,
                    CreatedBy = role.CreatedBy,
                    DefaultPageId = role.DefaultPageId,
                    Description = role.Description,
                    IsActive = role.IsActive,
                    IsDeleted = role.IsDeleted,
                    Name = role.NameAr,
                    Permissions = RolePermissionDbQuery.Select(x => x.SystemPagePermissionId).ToList(),
                    GeoZones = RoleGeoZonesDbQuery.Where(x => x.IsActive && !x.IsDeleted).Select(x => x.GeoZoneId).ToList()
                }

            } as IGetRoleForEditQueryResponse;
        }
    }
}

