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
    public class UpdateAgeSegmentCommandHandler : ICommandHandler<IUpdateAgeSegmentCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public UpdateAgeSegmentCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IUpdateAgeSegmentCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IAgeSegmentsRepository>();

                var ageSegment = repository.GetAgeSegmentById(command.AgeSegmentId);

                if (ageSegment == null)
                {
                    throw new Exception("AgeSegment not Found");
                }

                ageSegment.Name = command.Name;
                ageSegment.AgeFromDay = command.AgeFromDay;
                ageSegment.AgeFromMonth = command.AgeFromMonth;
                ageSegment.AgeFromYear = command.AgeFromYear;
                ageSegment.AgeFromInclusive = command.AgeFromInclusive;
                ageSegment.AgeToDay = command.AgeToDay;
                ageSegment.AgeToMonth = command.AgeToMonth;
                ageSegment.AgeToYear = command.AgeToYear;
                ageSegment.AgeToInclusive = command.AgeToInclusive;
                ageSegment.NeedExpert = command.NeedExpert;
                ageSegment.IsActive = command.IsActive;

                repository.UpdateAgeSegment(ageSegment);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
