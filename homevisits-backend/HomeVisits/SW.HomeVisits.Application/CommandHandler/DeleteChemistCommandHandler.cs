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
    public class DeleteChemistCommandHandler : ICommandHandler<IDeleteChemistCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        //private readonly ILog _log;

        public DeleteChemistCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }
        public void Handle(IDeleteChemistCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IUserRepository>();
                var user =  _userManager.FindByIdAsync(command.UserId.ToString()).GetAwaiter().GetResult();
                if (user == null)
                {
                    throw new Exception("user not Found");
                }
                var res =  _userManager.DeleteAsync(user).GetAwaiter().GetResult();
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

