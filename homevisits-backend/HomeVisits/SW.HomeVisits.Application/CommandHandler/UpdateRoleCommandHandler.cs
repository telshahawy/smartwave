using System;
using System.Linq;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class UpdateRoleCommandHandler : ICommandHandler<IUpdateRoleCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly RoleManager<Role> _roleManager;
        //private readonly ILog _log;

        public UpdateRoleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _roleManager = roleManager;
        }
        public void Handle(IUpdateRoleCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IRoleRepository>();
                var role = _roleManager.FindByIdAsync(command.RoleId.ToString()).GetAwaiter().GetResult();
                if (role == null)
                    throw new Exception("Role not Found");

                role.UpdateRole(command.NameAr, command.NameEn, command.Description, command.IsActive, command.DefaultPageId);
                foreach (var id in command.GeoZones)
                {
                    if (!role.GeoZones.Any(x => x.GeoZoneId == id))
                    {
                        var geoZone = new RoleGeoZone
                        {
                            RoleGeoZoneId = Guid.NewGuid(),
                            GeoZoneId = id,
                            RoleId = command.RoleId,
                        };
                        role.GeoZones.Add(geoZone);
                        repository.ChangeEntityStateToAdded(geoZone);
                    }
                    else
                    {
                        var geoZone = role.GeoZones.SingleOrDefault(x => x.GeoZoneId == id);
                        geoZone.IsDeleted = false;
                        repository.ChangeEntityStateToModified(geoZone);
                    }
                }

                var geoZonesToBeDeleted = role.GeoZones.Where(x => !command.GeoZones.Contains(x.GeoZoneId));
                foreach (var geoZone in geoZonesToBeDeleted)
                {
                    geoZone.IsDeleted = true;
                    repository.ChangeEntityStateToModified(geoZone);
                }

                foreach (var id in command.Permissions)
                {
                    if (!role.RolePermissions.Any(x => x.SystemPagePermissionId == id))
                    {
                        var permission = new RolePermission
                        {
                            RolePermissionId = Guid.NewGuid(),
                            SystemPagePermissionId = id,
                            RoleId = command.RoleId
                        };
                        role.RolePermissions.Add(permission);
                        repository.ChangeEntityStateToAdded(permission);
                    }
                    else
                    {
                        var permission = role.RolePermissions.SingleOrDefault(x => x.SystemPagePermissionId == id);
                        permission.IsDeleted = false;
                        repository.ChangeEntityStateToModified(permission);
                    }
                }

                var permissionsToBeDeleted = role.RolePermissions.Where(x => !command.Permissions.Contains(x.SystemPagePermissionId));
                foreach (var permission in permissionsToBeDeleted)
                {
                    permission.IsDeleted = true;
                    repository.ChangeEntityStateToModified(permission);
                }

                var res = _roleManager.UpdateAsync(role).GetAwaiter().GetResult();
                if (!res.Succeeded)
                {
                    throw new Exception(res.Errors.First().Code);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
