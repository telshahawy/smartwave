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
    //public class AddPermissionCommandHandler : ICommandHandler<IAddPermissionCommand>
    //{
    //    private readonly ILog _log;
    //    private readonly IHomeVisitsUnitOfWork _unitOfWork;

    //    public AddPermissionCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
    //    {
    //        _log = log;
    //        _unitOfWork = unitOfWork;
    //    }

    //    public void Handle(IAddPermissionCommand command)
    //    {
    //        try
    //        {
    //            Check.NotNull(command, nameof(command));
    //            var repository = _unitOfWork.Repository<IPermissionRepository>();
    //            var permissions = repository.GetPermissions().ToList();

    //            int permissionId, code;
    //            if (permissions == null || permissions.Count == 0)
    //            {
    //                code = 101;
    //                permissionId = 1;
    //            }
    //            else
    //            {
    //                code = permissions.OrderByDescending(p => p.PermissionId).FirstOrDefault().Code + 1;
    //                permissionId = permissions.OrderByDescending(p => p.PermissionId).FirstOrDefault().PermissionId + 1;
    //            }

    //            if (command != null && command.ControllerActionModelList != null && command.ControllerActionModelList.Count > 0)
    //            {
    //                foreach (var controllerActionModel in command.ControllerActionModelList)
    //                {
    //                    if (!permissions.Any(p => p.ControllerName.Trim().ToLower() == controllerActionModel.ControllerName.Trim().ToLower().Replace("controller", "") &&
    //                            p.ActionName.Trim().ToLower() == controllerActionModel.ActionName.Trim().ToLower()))
    //                    {
    //                        repository.AddPermission(new Permission
    //                        {
    //                            Code = code,
    //                            Position = 1,
    //                            SystemPageId = 1,
    //                            PermissionId = permissionId,
    //                            NameAr = controllerActionModel.ActionName,
    //                            NameEn = controllerActionModel.ActionName,
    //                            ActionName = controllerActionModel.ActionName,
    //                            ControllerName = controllerActionModel.ControllerName.Replace("Controller", ""),
    //                            IsActive = true,
    //                            IsNeedToAuthorized = false,
    //                            IsForDisplay = false
    //                        });
    //                        permissionId++;
    //                    }
    //                }
    //                _unitOfWork.SaveChanges();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
}
