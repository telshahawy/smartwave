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

    public class AddUserDeviceCommandHandler : ICommandHandler<IAddUserDeviceCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public AddUserDeviceCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IAddUserDeviceCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));

                var userDevice = new UserDevice
                {
                    UserDeviceId = command.UserDeviceId,
                    UserId = command.UserId,
                    DeviceSerialNumber = command.DeviceSerialNumber,
                    FireBaseDeviceToken = command.FireBaseDeviceToken,
                    CreatedAt = DateTime.Now
                };

                var repository = _unitOfWork.Repository<IUserRepository>();
                repository.AddUserDevice(userDevice);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
