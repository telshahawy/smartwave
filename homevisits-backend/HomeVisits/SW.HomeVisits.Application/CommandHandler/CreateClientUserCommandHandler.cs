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
    public class CreateClientUserCommandHandler : ICommandHandler<ICreateClientUserCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        //private readonly ILog _log;

        public CreateClientUserCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }

        public void Handle(ICreateClientUserCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();
                var roleRepository = _unitOfWork.Repository<IRoleRepository>();
                var role = roleRepository.FindRoleId(command.RoleId).GetAwaiter().GetResult();
                if (role == null)
                    throw new Exception(message: "Can't Retrieve Role");

                var userLatestNo = repository.GetLatestClientUserCode(command.ClientId) + 1;
                Check.NotNull(command, nameof(command));
                var user = User.CreateClientUser(command.UserId, command.UserName, command.Name, userLatestNo, command.PhoneNumber,
                    command.IsActive, command.ClientId, command.CreateBy,
                    command.GeoZones, command.RoleId);

                if (command.Permissions != null)
                {
                    foreach (var item in role.RolePermissions)
                    {
                        if (!command.Permissions.Any(p => p == item.SystemPagePermissionId))
                            user.AddUserExcludedRolePermissions(item.SystemPagePermissionId, item.RoleId);
                    }

                    foreach (var item in command.Permissions)
                    {
                        if (!role.RolePermissions.Any(p => p.SystemPagePermissionId == item))
                            user.AddUserAdditionalPermission(item);
                    }
                }

                var res = _userManager.CreateAsync(user, command.Password).GetAwaiter().GetResult();

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