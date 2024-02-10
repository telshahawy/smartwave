using System;
using System.Collections.Generic;
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
    public class UpdateClientUserCommandHandler : ICommandHandler<IUpdateClientUserCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        //private readonly ILog _log;

        public UpdateClientUserCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }
        public void Handle(IUpdateClientUserCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IUserRepository>();
                var user = _userManager.FindByIdAsync(command.UserId.ToString()).GetAwaiter().GetResult();
                if (user == null)
                    throw new Exception("user not Found");
                user.UpdateClientUser(command.Name, command.PhoneNumber, command.IsActive, command.RoleId);

                var roleRepository = _unitOfWork.Repository<IRoleRepository>();
                var role = roleRepository.FindRoleId(command.RoleId).Result;
                if (role == null)
                    throw new Exception(message: "Can't Retrieve Role");

                #region User GeoZones
                foreach (var id in command.GeoZonesIds)
                {
                    if (!user.GeoZones.Any(x => x.GeoZoneId == id))
                    {
                        var geoZone = new UserGeoZone
                        {
                            UserGeoZoneId = Guid.NewGuid(),
                            GeoZoneId = id,
                            UserId = command.UserId,
                            IsActive = true,
                            IsDeleted = false
                        };
                        user.GeoZones.Add(geoZone);
                        repository.ChangeEntityStateToAdded(geoZone);
                    }
                    else
                    {
                        var geoZone = user.GeoZones.SingleOrDefault(x => x.GeoZoneId == id);
                        geoZone.IsActive = true;
                        geoZone.IsDeleted = false;
                        repository.ChangeEntityStateToModified(geoZone);
                    }
                }

                var geoZonesToBeDeleted = user.GeoZones.Where(x => !command.GeoZonesIds.Contains(x.GeoZoneId));
                foreach (var geoZone in geoZonesToBeDeleted)
                {
                    geoZone.IsDeleted = true;
                    repository.ChangeEntityStateToModified(geoZone);
                }
                #endregion

                #region User Permission
                if (command.Permissions == null)
                    command.Permissions = new List<int>();
                foreach (var item in user.UserAdditionalPermissions)
                {
                    item.IsDeleted = true;
                    repository.ChangeEntityStateToModified(item);
                }

                foreach (var item in user.UserExcludedRolePermissions)
                {
                    item.IsDeleted = true;
                    repository.ChangeEntityStateToModified(item);
                }

                foreach (var item in role.RolePermissions)
                {
                    if (!command.Permissions.Any(p => p == item.SystemPagePermissionId))
                    {
                        var userExcludedRolePermission = user.AddUserExcludedRolePermissions(item.SystemPagePermissionId, item.RoleId);
                        if (userExcludedRolePermission != null)
                            repository.ChangeEntityStateToAdded(userExcludedRolePermission);
                    }
                }

                foreach (var item in command.Permissions)
                {
                    if (!role.RolePermissions.Any(p => p.SystemPagePermissionId == item))
                    {
                        var userAdditionalPermission = user.AddUserAdditionalPermission(item);
                        if (userAdditionalPermission != null)
                            repository.ChangeEntityStateToAdded(userAdditionalPermission);
                    }
                }
                #endregion

                var res = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
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

