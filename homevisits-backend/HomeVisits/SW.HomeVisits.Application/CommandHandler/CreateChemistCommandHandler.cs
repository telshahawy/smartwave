using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class CreateChemistCommandHandler : ICommandHandler<ICreateChemistCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        //private readonly ILog _log;

        public CreateChemistCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }

        public void Handle(ICreateChemistCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();
                var roleRepository = _unitOfWork.Repository<IRoleRepository>();
                var chemistRole = roleRepository.FindRoleByCode(Utility.ChemistRoleCode).Result;
                if (chemistRole == null)
                    throw new Exception(message: "Can't Retrieve Chemist Role");

                var chemistLatestNo = repository.GetLatestChemistCode(command.ClientId) + 1;
                Check.NotNull(command, nameof(command));
                var user = User.CreateChemist(command.UserId, command.UserName, command.Name, chemistLatestNo,
                    command.Gender, command.PhoneNumber, command.BirthDate, command.PersonalPhoto,
                    command.ExpertChemist, command.IsActive, command.ClientId,
                    command.JoinDate, command.DOB, command.CreatedBy, command.GeoZoneIds, chemistRole.RoleId);
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
