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

    public class AddOnHoldVisitCommandHandler : ICommandHandler<IAddOnHoldVisitCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public AddOnHoldVisitCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
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
        public void Handle(IAddOnHoldVisitCommand command)
        {
            try
            {
                //Check.NotNull(command, nameof(command));
                if ((command.CreateBy != null)&&(command.CreateBy.ToString() != ""))
                {
                    var onHoldVisit = new OnHoldVisit
                    {

                        OnHoldVisitId = command.OnHoldVisitId,
                        ChemistId = command.CreateBy,
                        CreatedAt = DateTime.Now,
                        IsCanceled = false,
                        TimeZoneFrameId = command.TimeZoneFrameGeoZoneId,
                        DeviceSerialNo = command.DeviceSerialNo,
                        NoOfPatients = command.NoOfPatients
                    };
                    var repository = _unitOfWork.Repository<IVisitRepository>();
                    repository.AddOnHoldVisit(onHoldVisit);
                    _unitOfWork.SaveChanges();

                }
                else
                {

                    var onHoldVisit1 = new OnHoldVisit
                    {

                        OnHoldVisitId = command.OnHoldVisitId,
                        ChemistId = null,
                        CreatedAt = DateTime.Now,
                        IsCanceled = false,
                        TimeZoneFrameId = command.TimeZoneFrameGeoZoneId,
                        DeviceSerialNo = null,
                        NoOfPatients = command.NoOfPatients
                    };
                    var repository = _unitOfWork.Repository<IVisitRepository>();
                    repository.AddOnHoldVisit(onHoldVisit1);
                    _unitOfWork.SaveChanges();
                }
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
