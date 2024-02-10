using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class CreatePatientAuthCommandHandler : ICommandHandler<ICreatePatientAuthCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        public CreatePatientAuthCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }
        public void Handle(ICreatePatientAuthCommand command)
        {
            try
            {
                var roleRepository = _unitOfWork.Repository<IRoleRepository>();
                var patientRole = roleRepository.FindRoleByCode(Utility.PatientRoleCode).Result;
                if (patientRole == null)
                    throw new Exception(message: "Can't Retrieve Patient Role");
                Check.NotNull(command, nameof(command));
                var user = User.CreatePatientLogin(command.UserId, command.UserName, command.Name, command.ClientId, command.IsActive, command.UserId, patientRole.RoleId, command.Code, command.PhoneNo);
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
