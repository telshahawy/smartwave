using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    class CreateSystemParametersCommandHandler : ICommandHandler<ICreateSystemParametersCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        private readonly UserManager<User> _userManager;
        public CreateSystemParametersCommandHandler(IHomeVisitsUnitOfWork unitOfWork,ILog log, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _userManager = userManager;
        }
        public void Handle(ICreateSystemParametersCommand command)
        {
            //
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<ISystemParametersRepository>();

                var systemParameter = new SystemParameter
                {
                    
                    EstimatedVisitDurationInMin = command.EstimatedVisitDurationInMin,
                    NextReserveHomevisitInDay = command.NextReserveHomevisitInDay,
                    RoutingSlotDurationInMin = command.RoutingSlotDurationInMin,
                    OptimizezonebeforeInMin = command?.OptimizezonebeforeInMin,
                    DefaultCountryId = command?.DefaultCountryId,
                    DefaultGovernorateId = command?.DefaultGovernorateId,
                    VisitApprovalBy = command.VisitApprovalBy,
                    VisitCancelBy = command.VisitCancelBy,
                    CallCenterNumber = command?.CallCenterNumber,
                    WhatsappBusinessLink=command?.WhatsappBusinessLink,
                    ClientId=command.ClientId,
                    CreateBy=command.CreateBy,
                    IsOptimizezonebefore=command.IsOptimizezonebefore,
                    IsSendPatientTimeConfirmation=command.IsSendPatientTimeConfirmation,
                    PrecautionsFile=command?.PrecautionsFile,
                    FileName=command?.FileName
                };

                repository.PresistNewSystemParameter(systemParameter);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
                //concarancy check
            }
        }
    }
}
