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
    public class DeleteRoleCommandHandler : ICommandHandler<IDeleteRoleCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly RoleManager<Role> _roleManager;
        //private readonly ILog _log;

        public DeleteRoleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _roleManager = roleManager;
        }
        public void Handle(IDeleteRoleCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IRoleRepository>();
                var role = _roleManager.FindByIdAsync(command.RoleId.ToString()).GetAwaiter().GetResult();
                if (role == null)
                {
                    throw new Exception("Role not Found");
                }
                var res = _roleManager.DeleteAsync(role).GetAwaiter().GetResult();
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
