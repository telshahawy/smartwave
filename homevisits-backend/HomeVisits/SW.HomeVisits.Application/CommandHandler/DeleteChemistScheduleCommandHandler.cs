using System;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class DeleteChemistScheduleCommandHandler : ICommandHandler<IDeleteChemistScheduleCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public DeleteChemistScheduleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IDeleteChemistScheduleCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();
                Check.NotNull(command, nameof(command));
                var schedule = repository.GetChemistSchedule(command.ChemistScheduleId);
                if(schedule == null)
                {
                    throw new Exception("schedule not found");
                }

                schedule.IsDeleted = true;
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

