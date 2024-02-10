using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class DeleteClientCommandHandler : ICommandHandler<IDeleteClientCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public DeleteClientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IDeleteClientCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IClientRepository>();

                repository.DeleteClient(command.ClientId);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
