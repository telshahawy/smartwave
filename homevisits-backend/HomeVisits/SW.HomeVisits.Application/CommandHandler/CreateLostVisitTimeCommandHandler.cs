using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{
    internal class CreateLostVisitTimeCommandHandler : ICommandHandler<ICreateLostVisitTimeCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public CreateLostVisitTimeCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(ICreateLostVisitTimeCommand command)
        {
            //access the domain services To Do Some Business 
            try
            {
                //Check.NotNull(command, nameof(command));
                //TimeSpan tsLostTime = TimeSpan.Parse(command.LostTime);
                //var lostVisitTime = new LostVisitTime(Guid.NewGuid(), command.ChemistId, command.VisitId, tsLostTime, command.CreatedBy, DateTime.Now);

                //var repository = _unitOfWork.Repository<ILostVisitTimeRepository>();
                //repository.PresistNewLostVisitTime(lostVisitTime);
                //_unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
