using System;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class UpdateChemistPermitCommandHandler : ICommandHandler<IUpdateChemistPermitCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public UpdateChemistPermitCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IUpdateChemistPermitCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();
                
                Check.NotNull(command, nameof(command));
                var permit = repository.GetChemistPermitById(command.ChemistPermitId);
                permit.EndTime = command.EndTime;
                permit.StartTime = command.StartTime;
                permit.PermitDate = command.PermitDate;
                repository.UpdateChmistPermit(permit);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
