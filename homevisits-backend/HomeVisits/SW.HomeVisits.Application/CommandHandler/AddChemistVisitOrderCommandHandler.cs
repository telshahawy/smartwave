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
    public class AddChemistVisitOrderCommandHandler : ICommandHandler<IAddChemistVisitOrderCommand>
    {
        private readonly ILog _log;
        private readonly IHomeVisitsUnitOfWork _unitOfWork;

        public AddChemistVisitOrderCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

        public void Handle(IAddChemistVisitOrderCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));

                var repository = _unitOfWork.Repository<IChemistVisitOrderRepository>();

                var oldChemistVisitOrder = repository.GetChemistVisitOrder(command.ChemistId, command.VisitId).ToList();
                if (oldChemistVisitOrder != null && oldChemistVisitOrder.Any())
                    repository.DeleteChemistVisitOrder(oldChemistVisitOrder);

                var chemistVisitOrder = new ChemistVisitOrder
                {
                    IsDeleted = false,
                    VisitId = command.VisitId,
                    CreatedDate = DateTime.Now,
                    Latitude = command.Latitude,
                    Longitude = command.Longitude,
                    ChemistId = command.ChemistId,
                    VisitOrder = command.VisitOrder,
                    TimeZoneFrameId = command.TimeZoneFrameId,
                    StartLangitude = command.StartLangitude,
                    StartLatitude = command.StartLatitude,
                    Distance = command.Distance,
                    DurationInTraffic = command.DurationInTraffic,
                    Duration = command.Duration,
                    ChemistVisitOrderId = command.ChemistVisitOrderId == Guid.Empty ? Guid.NewGuid() : command.ChemistVisitOrderId
                };
                repository.AddChemistVisitOrder(chemistVisitOrder);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
