using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Dtos;
using SW.HomeVisits.Application.Abstract.Queries;
using SW.HomeVisits.Application.Abstract.QueryResponses;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Infrastructure.ReadModel.DataModel;
using SW.HomeVisits.Infrastructure.ReadModel.QueryResponses;

namespace SW.HomeVisits.Infrastructure.ReadModel.QueryHandlers
{
    public class GetUserPermissionQueryHandler : IQueryHandler<IGetUserPermissionQuery, IGetUserPermissionQueryResponse>
    {
        private readonly ILog _log;
        private readonly HomeVisitsReadModelContext _context;

        public GetUserPermissionQueryHandler(HomeVisitsReadModelContext context, ILog log)
        {
            _log = log;
            _context = context;
        }

        public IGetUserPermissionQueryResponse Read(IGetUserPermissionQuery query)
        {
            IQueryable<UserPermissionView> dbQuery = _context.UserPermissionViews;
            if (query == null)
                throw new NullReferenceException(nameof(query));

            //var user = _context.UserViews.SingleOrDefault(x => x.UserId == query.UserId && x.ClientId == query.ClientId && !x.IsDeleted);
            //if (user == null)
            //    throw new Exception("User not found");

            //var role = _context.RolesViews.SingleOrDefault(p => p.RoleId == user.RoleId && p.ClientId == query.ClientId);
            //if (role == null)
            //    throw new Exception("User not assigned on role");

            var userPermission = dbQuery.Where(p => p.UserId == query.UserId && p.ClientId == query.ClientId && !p.PermissionIsDeleted).ToList();
            return new GetUserPermissionQueryResponse
            {
                //UserPermission = new UserPermissionDto
                //{
                SystemPagePermissionDtos = userPermission.Select(p => new SystemPagePermissionDto
                {
                    PermissionId = p.PermissionId,
                    PermissionCode = p.PermissionCode,
                    PermissionName = p.PermissionNameEn,
                    PermissionPosition = p?.PermissionPosition ?? 1,
                    SystemPageId = p.SystemPageId,
                    PermissionIsActive = p?.PermissionIsActive ?? true,
                    HasURL = p.SystemPageHasURL,
                    IsDisplayInMenue = p.SystemPageIsDisplayInMenue,
                    ParentId = p.SystemPageParentId,
                    SystemPageCode = p.SystemPageCode,
                    SystemPageName = p.SystemPageNameEn,
                    SystemPagePermissionId = p.SystemPagePermissionId,
                    SystemPagePosition = p?.SystemPagePosition ?? 1
                }).ToList()
                //}

            } as IGetUserPermissionQueryResponse;
        }
    }
}

