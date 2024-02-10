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
    public class CreateReasonCommandHandler : ICommandHandler<ICreateReasonCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public CreateReasonCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(ICreateReasonCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IReasonRepository>();

                var reason = new Reason
                {
                    Name = command.ReasonName,
                    Status = command.IsActive,
                    ReasonActionId = command.ReasonActionId,
                    VisitTypeActionId = command.VisitTypeActionId,
                    IsDeleted = false,
                    ClientId = command.ClientId
                };

                repository.PresistNewReason(reason);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
