using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Enum;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{

    public class AddSecondVisitCommandHandler : ICommandHandler<IAddSecondVisitCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public AddSecondVisitCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
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
        public void Handle(IAddSecondVisitCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));

                var repository = _unitOfWork.Repository<IVisitRepository>();
                var originVisit = repository.GetVisitById(command.OriginVisitId);
                var originVisitStatus = originVisit.VisitStatuses.OrderByDescending(v => v.CreationDate).FirstOrDefault();
                var visitLatestCode = repository.GetLatestVisitCode() + 1;
                var visitLatestNo = repository.GetLatestVisitNO() + 1;
                var visit = new Visit
                {
                    VisitId = command.VisitId,
                    VisitNo = visitLatestNo.ToString(),//GenerateRandomNo().ToString(),
                    VisitCode = visitLatestCode,
                    VisitTypeId = originVisit.VisitTypeId,
                    VisitDate = command.VisitDate,
                    PatientId = originVisit.PatientId,
                    PatientAddressId = originVisit.PatientAddressId,
                    ChemistId = command.ChemistId,
                    CreatedBy = originVisit.CreatedBy,
                    CreatedDate = DateTime.Now,
                    RelativeAgeSegmentId = originVisit.RelativeAgeSegmentId,
                    TimeZoneGeoZoneId = command.TimeZoneGeoZoneId,
                    PlannedNoOfPatients = originVisit.PlannedNoOfPatients,
                    RequiredTests = command.RequiredTests,
                    Comments = command.Comments,
                    OriginVisitId = originVisit.VisitId,
                    MinMinutes = command.MinMinutes,
                    MaxMinutes = command.MaxMinutes,
                    SelectBy = command.SelectBy,
                    IamNotSure = originVisit.IamNotSure,
                    RelativeDateOfBirth = originVisit.RelativeDateOfBirth,
                    VisitStatuses = new List<VisitStatus> {
                        new VisitStatus(Guid.NewGuid(), command.VisitId, command.Longitude, command.Latitude, command.DeviceSerialNumber, command.MobileBatteryPercentage,
                        (int)VisitActionTypes.New, (int)VisitStatusTypes.New, DateTime.Now, null, null, null, command.SecondVisitReason, null,originVisit.CreatedBy)
                    }
                };

                var visitAction = new VisitAction
                {
                    VisitActionId = Guid.NewGuid(),
                    VisitId = originVisit.VisitId,
                    CreationDate = DateTime.Now
                };

                if (originVisitStatus.VisitStatusTypeId == (int)VisitStatusTypes.New)
                {
                    repository.PresistNewVisitStatus(
                        new VisitStatus(Guid.NewGuid(), originVisit.VisitId, originVisitStatus.Longitude, originVisitStatus.Latitude, originVisitStatus.DeviceSerialNumber, originVisitStatus.MobileBatteryPercentage,
                        (int)VisitActionTypes.AcceptAndRequestSecondVisit, (int)VisitStatusTypes.Confirmed, DateTime.Now, null, null, null, null, null, command.CreatedBy)
                    );

                    visitAction.VisitActionTypeId = (int)VisitActionTypes.AcceptAndRequestSecondVisit;
                }
                else
                {
                    visitAction.VisitActionTypeId = (int)VisitActionTypes.RequestSecondVisit;
                }

                repository.AddVisit(visit);
                repository.AddNewVisitAction(visitAction);

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
