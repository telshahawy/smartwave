using System;
using System.Collections.Generic;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Enums;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class DuplicateChemistScheduleCommandHandler : ICommandHandler<IDuplicateChemistScheduleCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public DuplicateChemistScheduleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IDuplicateChemistScheduleCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();

                Check.NotNull(command, nameof(command));
                var schedule = repository.GetChemistSchedule(command.ChemistScheduleId);
                if (schedule == null)
                {
                    throw new Exception("schedule not found");
                }
                var newSchedule = new ChemistSchedule
                {
                    ChemistScheduleId = command.NewChemistScheduleId,
                    ChemistAssignedGeoZoneId = schedule.ChemistAssignedGeoZoneId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = command.CreatedBy,
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    IsActive = true,
                    StartLangitude = schedule.StartLangitude,
                    StartLatitude = schedule.StartLatitude,
                    IsDeleted = false,
                };
                newSchedule.ScheduleDays = new List<ChemistScheduleDay>();
                foreach(var day in schedule.ScheduleDays)
                {
                    newSchedule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = newSchedule.ChemistScheduleId,
                        Day = day.Day,
                        StartTime = day.StartTime,
                        EndTime = day.EndTime
                    });
                }
                repository.PresistNewChmistSchedule(newSchedule);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

