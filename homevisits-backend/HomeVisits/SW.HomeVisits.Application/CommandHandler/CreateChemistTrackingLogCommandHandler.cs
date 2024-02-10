using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    internal class CreateChemistTrackingLogCommandHandler : ICommandHandler<ICreateChemistTrackingLogCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public CreateChemistTrackingLogCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(ICreateChemistTrackingLogCommand command)
        {
            //access the domain services To Do Some Business 
            try
            {
                Check.NotNull(command, nameof(command));
                var chemistTrackingLog = new ChemistTrackingLog(Guid.NewGuid(), command.ChemistId, command.Longitude, command.Latitude,
                    command.DeviceSerialNumber, command.MobileBatteryPercentage, command.UserName, DateTime.Now);

                var repository = _unitOfWork.Repository<IChemistTrackingLogRepository>();
                repository.PresistNewChemistTrackingLog(chemistTrackingLog);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
