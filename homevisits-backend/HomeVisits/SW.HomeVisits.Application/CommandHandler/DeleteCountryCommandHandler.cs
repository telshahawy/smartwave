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
    public class DeleteCountryCommandHandler : ICommandHandler<IDeleteCountryCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public DeleteCountryCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IDeleteCountryCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<ICountryRepository>();

                repository.DeleteCountry(command.CountryId);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
