using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    internal class CreateVisitStatusCommandHandler : ICommandHandler<ICreateVisitStatusCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public CreateVisitStatusCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(ICreateVisitStatusCommand command)
        {
            //access the domain services To Do Some Business 
            try
            {
                Check.NotNull(command, nameof(command));

                var repository = _unitOfWork.Repository<IVisitRepository>();

                if (command.VisitActionTypeId != (int)VisitActionTypes.ReassignChemist)
                {
                    var visitStatus = new VisitStatus(Guid.NewGuid(), command.VisitId, command.Longitude, command.Latitude, command.DeviceSerialNumber,
                       command.MobileBatteryPercentage, command.VisitActionTypeId, command.VisitStatusTypeId, DateTime.Now,
                       command.ActualNoOfPatients, command.NoOfTests, command.IsAddressVerified, command.ReasonId, command.Comments,command.CreatedBy);
                    
                    repository.PresistNewVisitStatus(visitStatus);
                }

                VisitAction visitAction = new VisitAction
                {
                    VisitActionId = Guid.NewGuid(),
                    VisitId = command.VisitId,
                    VisitActionTypeId = command.VisitActionTypeId,
                    CreationDate = DateTime.Now,
                    ReasonId = command.ReasonId,
                    Comments = command.Comments

                };
                repository.AddNewVisitAction(visitAction);

                if (command.VisitActionTypeId == (int)VisitActionTypes.ReassignChemist)
                {
                    var visit = repository.GetVisitById(command.VisitId);
                    visit.TimeZoneGeoZoneId = command.TimeZoneGeoZoneId.GetValueOrDefault();
                    visit.VisitDate = command.VisitDate.GetValueOrDefault();
                    visit.ChemistId = command.ChemistId;

                    //Update Visit
                    repository.UpdateVisit(visit);
                }

                if (command.IsAddressVerified.HasValue && command.IsAddressVerified.Value)
                {
                    var patientRepository = _unitOfWork.Repository<IPatientRepository>();

                    var visit = repository.GetVisitById(command.VisitId);
                    visit.PatientAddress.IsConfirmed = command.IsAddressVerified.Value;
                    patientRepository.UpdatePatientAddress(visit.PatientAddress);
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
