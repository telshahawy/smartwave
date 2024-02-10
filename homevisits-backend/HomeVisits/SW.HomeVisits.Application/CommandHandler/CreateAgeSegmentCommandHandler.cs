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
    public class CreateAgeSegmentCommandHandler : ICommandHandler<ICreateAgeSegmentCommand>
    {

        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;

        public CreateAgeSegmentCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(ICreateAgeSegmentCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IAgeSegmentsRepository>();

                var ageSegmentLatestNo = repository.GetLatestAgeSegmentCode(command.ClientId) + 1;
                var ageSegment = new AgeSegment
                {
                    AgeSegmentId = command.AgeSegmentId,
                    ClientId = command.ClientId,
                    Name = command.Name,
                    Code = ageSegmentLatestNo,
                    AgeFromDay = command.AgeFromDay,
                    AgeFromMonth = command.AgeFromMonth,
                    AgeFromYear = command.AgeFromYear,
                    AgeFromInclusive = command.AgeFromInclusive,
                    AgeToDay = command.AgeToDay,
                    AgeToMonth = command.AgeToMonth,
                    AgeToYear = command.AgeToYear,
                    AgeToInclusive = command.AgeToInclusive,
                    NeedExpert = command.NeedExpert,
                    CreatedBy = command.CreatedBy,
                    CreatedAt = DateTime.Now,
                    IsActive = command.IsActive
                };

                repository.PresistNewAgeSegment(ageSegment);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
