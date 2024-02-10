using System;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class DeleteChemistPermitCommandHandler : ICommandHandler<IDeleteChemistPermitCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public DeleteChemistPermitCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IDeleteChemistPermitCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();

                Check.NotNull(command, nameof(command));
                repository.DeleteChmistPermit(command.ChemistPermitId);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
