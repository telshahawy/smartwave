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
    public class UpdateChemistScheduleCommandHandler : ICommandHandler<IUpdateChemistScheduleCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public UpdateChemistScheduleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IUpdateChemistScheduleCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();

                Check.NotNull(command, nameof(command));
                var schendule = repository.GetChemistSchedule(command.ChemistScheduleId);
                if(schendule == null)
                {
                    throw new Exception("Schedule not found");
                }
                schendule.StartDate = command.StartDate;
                schendule.EndDate = command.EndDate;
                schendule.StartLangitude = command.StartLangitude;
                schendule.StartLatitude = command.StartLatitude;
                foreach(var day in schendule.ScheduleDays)
                {
                    repository.ChangeEntityStateToDeleted(day);
                }
                schendule.ScheduleDays.Clear();
                if (command.SunStart != null && command.SunEnd != null)
                {
                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Sun,
                        StartTime = command.SunStart.GetValueOrDefault(),
                        EndTime = command.SunEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                if (command.MonStart != null && command.MonEnd != null)
                {
                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Mon,
                        StartTime = command.MonStart.GetValueOrDefault(),
                        EndTime = command.MonEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                if (command.TueStart != null && command.TueEnd != null)
                {
                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Tue,
                        StartTime = command.TueStart.GetValueOrDefault(),
                        EndTime = command.TueEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                if (command.WedStart != null && command.WedEnd != null)
                {

                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Wed,
                        StartTime = command.WedStart.GetValueOrDefault(),
                        EndTime = command.WedEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                if (command.ThuStart != null && command.TueEnd != null)
                {
                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Thu,
                        StartTime = command.ThuStart.GetValueOrDefault(),
                        EndTime = command.TueEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                if (command.FriStart != null && command.FriEnd != null)
                {
                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Fri,
                        StartTime = command.FriStart.GetValueOrDefault(),
                        EndTime = command.FriEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                if (command.SatStart != null && command.SatEnd != null)
                {
                    var schedule = new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = schendule.ChemistScheduleId,
                        Day = (int)Days.Sat,
                        StartTime = command.SatStart.GetValueOrDefault(),
                        EndTime = command.SatEnd.GetValueOrDefault()
                    };
                    schendule.ScheduleDays.Add(schedule);
                    repository.ChangeEntityStateToAdded(schedule);
                }
                repository.UpdateChemistSchedule(schendule);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

