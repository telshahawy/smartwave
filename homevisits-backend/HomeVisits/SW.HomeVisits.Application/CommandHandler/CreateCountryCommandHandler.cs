using System;
using System.Linq;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class CreateCountryCommandHandler : ICommandHandler<ICreateCountryCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public CreateCountryCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(ICreateCountryCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<ICountryRepository>();
              //var nameExist=  repository.CountryNameExists(command.CountryNameEn, command.ClientId, Guid.Empty);
              //  if (!nameExist)
              //  {

                    var countryLatestNo = repository.GetLatestCountryCode(command.ClientId) + 1;
                    var country = new Country
                    {
                        CountryId = command.CountryId,
                        ClientId = command.ClientId,
                        CountryNameEn = command.CountryNameEn,
                        CountryNameAr = command.CountryNameAr,
                        Code = countryLatestNo,
                        MobileNumberLength = command.MobileNumberLength,
                        CreatedBy = command.CreatedBy,
                        CreatedAt = DateTime.Now,
                        IsActive = command.IsActive
                    };
                    repository.PresistNewCountry(country);
                    _unitOfWork.SaveChanges();
               // }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
