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
    public class UpdateClientUserPermissionCommandHandler : ICommandHandler<IUpdateClientUserPermissionCommand>
    {
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        private readonly IHomeVisitsUnitOfWork _unitOfWork;

        public UpdateClientUserPermissionCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }

        public void Handle(IUpdateClientUserPermissionCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IUserRepository>();
                var user = _userManager.FindByIdAsync(command.UserId.ToString()).GetAwaiter().GetResult();
                if (user == null)
                    throw new Exception("user not Found");

                var userAdditionalPermissions = repository.GetUserAdditionalPermission(user.UserId).ToList();
                var userExcludedRolePermissions = repository.GetUserExcludedRolePermission(user.UserId).ToList();

                var roleRepository = _unitOfWork.Repository<IRoleRepository>();
                var roleSystemPagePermissions = roleRepository.GetRoleSystemPagePermissions(user.RoleId).ToList();

                #region User Permission
                List<UserAdditionalPermission> newUserAdditionalPermission = new List<UserAdditionalPermission>();
                List<UserExcludedRolePermission> newUserExcludedRolePermission = new List<UserExcludedRolePermission>();
                if (command.Permissions == null)
                {
                    if (roleSystemPagePermissions != null && roleSystemPagePermissions.Count > 0)
                    {
                        foreach (var item in roleSystemPagePermissions)
                        {
                            if (!userExcludedRolePermissions.Any(p => p.SystemPagePermissionId == item.SystemPagePermissionId))
                            {
                                newUserExcludedRolePermission.Add(new UserExcludedRolePermission
                                {
                                    IsDeleted = false,
                                    RoleId = user.RoleId,
                                    UserId = user.UserId,
                                    SystemPagePermissionId = item.SystemPagePermissionId
                                });
                            }
                        }
                        repository.AddUserExcludedRolePermission(newUserExcludedRolePermission);
                    }
                    if (userAdditionalPermissions != null && userAdditionalPermissions.Count > 0)
                    {
                        userAdditionalPermissions.ForEach(p => p.IsDeleted = true);
                        repository.UpdateUserAdditionalPermission(userAdditionalPermissions);
                    }
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    if (userAdditionalPermissions != null && userAdditionalPermissions.Count > 0)
                    {
                        userAdditionalPermissions.ForEach(p => p.IsDeleted = true);
                        repository.UpdateUserAdditionalPermission(userAdditionalPermissions);
                    }
                    if (userExcludedRolePermissions != null && userExcludedRolePermissions.Count > 0)
                    {
                        userExcludedRolePermissions.ForEach(p => p.IsDeleted = true);
                        repository.UpdateUserExcludedRolePermission(userExcludedRolePermissions);
                    }
                    _unitOfWork.SaveChanges();

                    foreach (var item in roleSystemPagePermissions)
                    {
                        if (!command.Permissions.Any(p => p == item.SystemPagePermissionId))
                        {
                            newUserExcludedRolePermission.Add(new UserExcludedRolePermission
                            {
                                IsDeleted = false,
                                RoleId = user.RoleId,
                                UserId = user.UserId,
                                SystemPagePermissionId = item.SystemPagePermissionId
                            });
                        }
                    }
                    repository.AddUserExcludedRolePermission(newUserExcludedRolePermission);

                    foreach (var item in command.Permissions)
                    {
                        if (!roleSystemPagePermissions.Any(p => p.SystemPagePermissionId == item))
                        {
                            newUserAdditionalPermission.Add(new UserAdditionalPermission
                            {
                                IsDeleted = false,
                                UserId = user.UserId,
                                SystemPagePermissionId = item
                            });
                        }
                    }
                    repository.AddUserAdditionalPermission(newUserAdditionalPermission);
                    _unitOfWork.SaveChanges();
                }


                //foreach (var item in userAdditionalPermissions)
                //{
                //    if (!command.Permissions.Any(p => p == item.SystemPagePermissionId))
                //        item.IsDeleted = true;
                //}
                //repository.UpdateUserAdditionalPermission(userAdditionalPermissions);

                //foreach (var item in userExcludedRolePermissions)
                //{
                //    if (command.Permissions.Any(p => p == item.SystemPagePermissionId))
                //        item.IsDeleted = true;
                //}
                //repository.UpdateUserExcludedRolePermission(userExcludedRolePermissions);
                //_unitOfWork.SaveChanges();

                //foreach (var item in roleSystemPagePermissions)
                //{
                //    if (!command.Permissions.Any(p => p == item.SystemPagePermissionId))
                //    {
                //        var userExcludedRolePermission = user.AddUserExcludedRolePermissions(item.SystemPagePermissionId, item.RoleId);
                //        repository.ChangeEntityStateToAdded(userExcludedRolePermission);
                //    }
                //}

                //foreach (var item in command.Permissions)
                //{
                //    if (!roleSystemPagePermissions.Any(p => p.SystemPagePermissionId == item))
                //    {
                //        var userAdditionalPermission = user.AddUserAdditionalPermission(item);
                //        repository.ChangeEntityStateToAdded(userAdditionalPermission);
                //    }
                //}
                #endregion

                //var res = _userManager.UpdateAsync(user).GetAwaiter().GetResult();
                //if (!res.Succeeded)
                //{
                //    throw new Exception(res.Errors.First().Code);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

