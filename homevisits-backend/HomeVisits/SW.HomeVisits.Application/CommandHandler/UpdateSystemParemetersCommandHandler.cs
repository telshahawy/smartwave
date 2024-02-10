using SW.Framework.Cqrs;
using SW.HomeVisits.Application.Abstract.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using SW.HomeVisits.Domain.Repositories;
using Common.Logging;
using SW.Framework.Validation;
using SW.HomeVisits.Domain.Entities;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class UpdateSystemParemetersCommandHandler : ICommandHandler<IUpdateSystemParemetersCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public UpdateSystemParemetersCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IUpdateSystemParemetersCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<ISystemParametersRepository>();
               
                var systemParameter = new SystemParameter
                {
                    ClientId = command.ClientId,
                    CreateBy = command.CreateBy,
                    DefaultCountryId = command?.DefaultCountryId,
                    DefaultGovernorateId = command?.DefaultGovernorateId,
                    EstimatedVisitDurationInMin = command.EstimatedVisitDurationInMin,
                    NextReserveHomevisitInDay = command.NextReserveHomevisitInDay,
                    OptimizezonebeforeInMin = command?.OptimizezonebeforeInMin,
                    RoutingSlotDurationInMin = command.RoutingSlotDurationInMin,
                    VisitApprovalBy = command.VisitApprovalBy,
                    VisitCancelBy = command.VisitCancelBy,
                    WhatsappBusinessLink = command?.WhatsappBusinessLink,
                    PrecautionsFile = command?.PrecautionsFile,
                    CallCenterNumber = command?.CallCenterNumber,
                    IsOptimizezonebefore = command?.IsOptimizezonebefore,
                    IsSendPatientTimeConfirmation = command?.IsSendPatientTimeConfirmation,
                    FileName=command?.FileName
                };
                repository.UpdateSystemParameter(systemParameter);
                _unitOfWork.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }
    }
}
