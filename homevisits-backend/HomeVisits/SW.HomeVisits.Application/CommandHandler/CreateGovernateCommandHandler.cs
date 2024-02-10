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
    public class CreateGovernateCommandHandler : ICommandHandler<ICreateGovernateCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public CreateGovernateCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(ICreateGovernateCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IGovernatRepository>();

                var governateLatestNo = repository.GetLatestGovernateCode() + 1;
                var governate = new Governate
                {
                    GovernateId = command.GovernateId,
                    CountryId = command.CountryId,
                    GoverNameEn = command.GovernateNameEn,
                    GoverNameAr = command.GovernateNameAr,
                    Code = governateLatestNo,
                    CustomerServiceEmail = command.CustomerServiceEmail,
                    CreatedBy = command.CreatedBy,
                    CreatedAt = DateTime.Now,
                    IsActive = command.IsActive
                };

                repository.PresistNewGovernate(governate);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
