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
    public class CreateRoleCommandHandler : ICommandHandler<ICreateRoleCommand>
    {
        private readonly ILog _log;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHomeVisitsUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _roleManager = roleManager;
        }

        public void Handle(ICreateRoleCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IRoleRepository>();

                var roleLatestNo = repository.GetLatestRoleCode(command.ClientId) + 1;
                var role = Role.CreateNewRole(command.RoleId, command.ClientId,
                    roleLatestNo,
                    command.NameAr, command.NameEn,
                    command.Description, command.IsActive,
                    command.CreatedBy,command.DefaultPageId);

                foreach(var permissionId in command.Permissions)
                {
                    role.AddPermission(permissionId);
                }

                foreach (var geoZoneId in command.GeoZones)
                {
                    role.AddGeoZone(geoZoneId);
                }

                var res = _roleManager.CreateAsync(role).GetAwaiter().GetResult();

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
