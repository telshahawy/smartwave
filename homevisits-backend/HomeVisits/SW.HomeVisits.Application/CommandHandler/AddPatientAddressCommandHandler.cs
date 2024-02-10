using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class AddPatientAddressCommandHandler : ICommandHandler<IAddPatientAddressCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public AddPatientAddressCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IAddPatientAddressCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IPatientRepository>();
               var latestCode= repository.GetLatestPatientAddressCode() + 1;
                var address = new PatientAddress
                {
                    PatientAddressId = command.PatientAddressId,
                    Code= latestCode,
                    PatientId = command.PatientId,
                    Building = command.Building,
                    AdditionalInfo = command.AdditionalInfo,
                    CreateBy = command.CreateBy,
                    CreatedAt = DateTime.Now,
                    Flat = command.Flat,
                    Floor = command.Floor,
                    GeoZoneId = command.GeoZoneId,
                    IsConfirmed = false,
                    Latitude = command.Latitude,
                    Longitude = command.Longitude,
                    street = command.street,
                    LocationUrl = command.LocationUrl
                };

                
                repository.AddPatientAddress(address);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
