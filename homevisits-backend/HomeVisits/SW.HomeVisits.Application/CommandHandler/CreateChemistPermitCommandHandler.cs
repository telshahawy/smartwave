using System;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class CreateChemistPermitCommandHandler : ICommandHandler<ICreateChemistPermitCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public CreateChemistPermitCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(ICreateChemistPermitCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();

                Check.NotNull(command, nameof(command));
                var permit = new ChemistPermit
                {
                    ChemistPermitId = command.ChemistPermitId,
                    ChemistId = command.ChemistId,
                    PermitDate = command.PermitDate,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = command.CreatedBy,
                    EndTime = command.EndTime,
                    StartTime = command.StartTime
                };
                repository.PresistNewChmistPermit(permit);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
