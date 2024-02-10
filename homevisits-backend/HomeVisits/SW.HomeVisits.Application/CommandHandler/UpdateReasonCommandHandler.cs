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
    public class UpdateReasonCommandHandler : ICommandHandler<IUpdateReasonCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public UpdateReasonCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IUpdateReasonCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IReasonRepository>();

                var reason = repository.GetReasonById(command.ReasonId);

                if (reason == null)
                {
                    throw new Exception("Reason not Found");
                }

                reason.Name = command.ReasonName;
                reason.Status = command.IsActive;
                reason.ReasonActionId = command.ReasonActionId;
                reason.VisitTypeActionId = command.VisitTypeActionId;

                repository.UpdateReason(reason);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
