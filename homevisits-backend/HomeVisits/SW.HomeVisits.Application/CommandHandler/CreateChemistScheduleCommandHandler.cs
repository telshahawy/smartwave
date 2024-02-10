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
    public class CreateChemistScheduleCommandHandler : ICommandHandler<ICreateChemistScheduleCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public CreateChemistScheduleCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(ICreateChemistScheduleCommand command)
        {
            try
            {
                var repository = _unitOfWork.Repository<IUserRepository>();

                Check.NotNull(command, nameof(command));
                var Schendule = new ChemistSchedule
                {
                    ChemistScheduleId = command.ChemistScheduleId,
                    ChemistAssignedGeoZoneId = command.AssignedChemistGeoZoneId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = command.CreatedBy,
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    IsActive = true,
                    StartLangitude = command.StartLangitude,
                    StartLatitude = command.StartLatitude,
                    IsDeleted = false,
                };
                Schendule.ScheduleDays = new List<ChemistScheduleDay>();
                if (command.SunStart != null && command.SunEnd != null)
                {
                    if (command.SunStart >= command.SunEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Sun,
                        StartTime = command.SunStart.GetValueOrDefault(),
                        EndTime = command.SunEnd.GetValueOrDefault()
                    });
                }
                if (command.MonStart != null && command.MonEnd != null)
                {
                    if (command.MonStart >= command.MonEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Mon,
                        StartTime = command.MonStart.GetValueOrDefault(),
                        EndTime = command.MonEnd.GetValueOrDefault()
                    });
                }
                if (command.TueStart != null && command.TueEnd != null)
                {
                    if (command.TueStart >= command.TueEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Tue,
                        StartTime = command.TueStart.GetValueOrDefault(),
                        EndTime = command.TueEnd.GetValueOrDefault()
                    });
                }
                if (command.WedStart != null && command.WedEnd != null)
                {
                    if (command.WedStart >= command.WedEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Wed,
                        StartTime = command.WedStart.GetValueOrDefault(),
                        EndTime = command.WedEnd.GetValueOrDefault()
                    });
                }
                if (command.ThuStart != null && command.TueEnd != null)
                {
                    if (command.ThuStart >= command.ThuEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Thu,
                        StartTime = command.ThuStart.GetValueOrDefault(),
                        EndTime = command.TueEnd.GetValueOrDefault()
                    });
                }
                if (command.FriStart != null && command.FriEnd != null)
                {
                    if (command.FriStart >= command.FriEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Fri,
                        StartTime = command.FriStart.GetValueOrDefault(),
                        EndTime = command.FriEnd.GetValueOrDefault()
                    });
                }
                if (command.SatStart != null && command.SatEnd != null)
                {
                    if (command.SatStart >= command.SatEnd) throw new Exception("Start time cannot be greater than end time");
                    Schendule.ScheduleDays.Add(new ChemistScheduleDay
                    {
                        ChemistScheduleDayId = Guid.NewGuid(),
                        ChemistScheduleId = Schendule.ChemistScheduleId,
                        Day = (int)Days.Sat,
                        StartTime = command.SatStart.GetValueOrDefault(),
                        EndTime = command.SatEnd.GetValueOrDefault()
                    });
                }
                repository.PresistNewChmistSchedule(Schendule);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}

