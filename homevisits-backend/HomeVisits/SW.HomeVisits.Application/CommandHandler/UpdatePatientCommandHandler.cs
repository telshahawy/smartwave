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
    public class UpdatePatientCommandHandler : ICommandHandler<IUpdatePatientCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public UpdatePatientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IUpdatePatientCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IPatientRepository>();
                DateTime dateOfBirth = (DateTime)command.BirthDate;
                // Save today's date.
                var today = DateTime.Today;
                int age = 0;
                age = DateTime.Now.Subtract(dateOfBirth).Days;
                age /= 365;
                var patient = new Patient
                {
                    PatientId = command.PatientId,
                    PatientNo = command.PatientNo,
                    DOB = command.BirthDate == null ? "DOB" : age.ToString(),
                    Gender = command.Gender,
                    ClientId = command.ClientId,
                    Name = command.Name,
                    BirthDate = command.BirthDate,
                    IsDeleted = command.IsDeleted,
                    Addresses = command.Addresses.Select(item => new PatientAddress
                    {

                        PatientAddressId = item.PatientAddressId,
                        PatientId = command.PatientId,
                        Building = item.Building,
                        AdditionalInfo = item.AdditionalInfo,
                        CreateBy = item.CreateBy,
                        CreatedAt = DateTime.Now,
                        Flat = item.Flat,
                        Floor = item.Floor,
                        GeoZoneId = item.GeoZoneId,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        street = item.street,
                        LocationUrl = item.LocationUrl

                    }).ToList(),
                    Phones = command.Phones.Select(p => new PatientPhone
                    {

                        PatientPhoneId = p.PatientPhoneId,
                        PatientId = command.PatientId,
                        PhoneNumber = p.PhoneNumber,
                        CreateBy = p.CreatedBy,
                        CreatedAt = DateTime.Now
                    }).ToList()
                };

                repository.UpdatePatient(patient);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
