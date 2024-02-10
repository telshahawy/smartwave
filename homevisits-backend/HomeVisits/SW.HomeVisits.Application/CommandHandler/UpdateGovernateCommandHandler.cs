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
    public class UpdateGovernateCommandHandler : ICommandHandler<IUpdateGovernateCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public UpdateGovernateCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IUpdateGovernateCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IGovernatRepository>();

                var governate = repository.GetGovernateById(command.GovernateId);

                if (governate == null)
                {
                    throw new Exception("Governate not Found");
                }

                governate.GoverNameEn = command.GovernateNameEn;
                governate.GoverNameAr = command.GovernateNameEn;
                governate.CustomerServiceEmail = command.CustomerServiceEmail;
                governate.IsActive = command.IsActive;
                governate.Code = governate.Code;
                governate.CountryId = command.CountryId;
                repository.UpdateGovernate(governate);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
