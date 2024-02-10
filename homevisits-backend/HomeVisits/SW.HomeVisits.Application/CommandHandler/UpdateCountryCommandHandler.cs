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
    public class UpdateCountryCommandHandler : ICommandHandler<IUpdateCountryCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public UpdateCountryCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IUpdateCountryCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<ICountryRepository>();

                var country = repository.GetCountryById(command.CountryId);

                if (country == null)
                {
                    throw new Exception("Country not Found");
                }

                country.CountryNameEn = command.CountryNameEn;
                country.CountryNameAr = command.CountryNameEn;
                country.MobileNumberLength = command.MobileNumberLength;
                country.IsActive = command.IsActive;
                country.Code = country.Code;
                repository.UpdateCountry(country);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
