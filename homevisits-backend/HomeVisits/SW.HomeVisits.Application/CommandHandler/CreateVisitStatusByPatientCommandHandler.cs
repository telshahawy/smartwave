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
    internal class CreateVisitStatusByPatientCommandHandler : ICommandHandler<ICreateVisitStatusByPatientCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public CreateVisitStatusByPatientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(ICreateVisitStatusByPatientCommand command)
        {
            //access the domain services To Do Some Business 
            try
            {
                Check.NotNull(command, nameof(command));

                var repository = _unitOfWork.Repository<IVisitRepository>();

                var visit = repository.GetVisitById(command.VisitId);
                if (command.VisitActionTypeId != (int)VisitActionTypes.ReassignChemist)
                {
                    var visitStatus = new VisitStatus(Guid.NewGuid(), command.VisitId, null, null, null,
                       null, command.VisitActionTypeId, command.VisitStatusTypeId, DateTime.Now,
                       null, null, null, null, null, visit.PatientId);

                    repository.PresistNewVisitStatus(visitStatus);
                }

                VisitAction visitAction = new VisitAction
                {
                    VisitActionId = Guid.NewGuid(),
                    VisitId = command.VisitId,
                    VisitActionTypeId = command.VisitActionTypeId,
                    CreationDate = DateTime.Now
                };
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
