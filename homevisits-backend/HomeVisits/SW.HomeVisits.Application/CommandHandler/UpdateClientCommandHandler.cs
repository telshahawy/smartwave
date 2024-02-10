using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.CommandHandler
{
   public class UpdateClientCommandHandler: ICommandHandler<IUpdateClientCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public UpdateClientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IUpdateClientCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var repository = _unitOfWork.Repository<IClientRepository>();

                var client = repository.GetClientByID(command.ClientId);

                if (client == null)
                {
                    throw new Exception("Client not Found");
                }

                client.ClientName = command.ClientName;
                client.ClientCode = command.ClientCode;
                client.URLName = command.URLName;
                client.DisplayName = command.DisplayName;
                client.CountryId = command.CountryId;
                client.Logo = command.Logo;

                client.IsActive = command.IsActive;

                repository.UpdateClient(client);
                _unitOfWork.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
