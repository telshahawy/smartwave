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
    public class AddPatientCommandHandler : ICommandHandler<IAddPatientCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public AddPatientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public int GenerateRandomNo()
        {
            int _min = 100;
            int _max = 999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public void Handle(IAddPatientCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IPatientRepository>();
                DateTime dateOfBirth =(DateTime) command.BirthDate;
                // Save today's date.
                var today = DateTime.Today;
                int age = 0;
                age = DateTime.Now.Subtract(dateOfBirth).Days;
                age /= 365;

                // Go back to the year in which the person was born in case of a leap year
                if (dateOfBirth.Date > today.AddYears(-age)) age--;

                if (command.Addresses!=null)
                {
                    var patient = new Patient
                    {
                        PatientId = command.PatientId,
                        PatientNo = GenerateRandomNo().ToString(),
                        DOB = command.BirthDate == null?"DOB": age.ToString(),
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

                    repository.AddPatient(patient);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    var patient = new Patient
                    {
                        PatientId = command.PatientId,
                        PatientNo = GenerateRandomNo().ToString(),
                        DOB = age.ToString(),
                        Gender = command.Gender,
                        ClientId = command.ClientId,
                        Name = command.Name,
                        BirthDate = command.BirthDate,
                        IsDeleted = command.IsDeleted,
                      
                        Phones = command.Phones.Select(p => new PatientPhone
                        {

                            PatientPhoneId = p.PatientPhoneId,
                            PatientId = command.PatientId,
                            PhoneNumber = p.PhoneNumber,
                            CreateBy = p.CreatedBy,
                            CreatedAt = DateTime.Now
                        }).ToList()
                    };

                    repository.AddPatient(patient);
                    _unitOfWork.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
