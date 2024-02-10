using Common.Logging;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW.HomeVisits.Application.CommandHandler
{
    public class AddClientCommandHandler : ICommandHandler<IAddClientCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        //private readonly ILog _log;

        public AddClientCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }

      

        public void Handle(IAddClientCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var client = new Client
                {
                    ClientId = command.ClientId,
                    CountryId = command.CountryId,
                    ClientName = command.ClientName,
                    ClientCode = command.ClientCode,
                    URLName = command.URLName,
                    DisplayName = command.DisplayName,
                    Logo = command.Logo,
                    IsActive=command.IsActive
                };

                var repository = _unitOfWork.Repository<IClientRepository>();
                repository.AddClient(client);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
