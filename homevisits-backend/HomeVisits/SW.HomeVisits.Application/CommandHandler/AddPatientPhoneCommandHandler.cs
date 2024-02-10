using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.AspNetCore.Identity;
using SW.Framework.Cqrs;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Domain.Entities;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.CommandHandler
{

    public class AddPatientPhoneCommandHandler: ICommandHandler<IAddPatientPhoneCommand>
    {
        private readonly IHomeVisitsUnitOfWork _unitOfWork;
        private readonly ILog _log;
        public AddPatientPhoneCommandHandler(IHomeVisitsUnitOfWork unitOfWork, ILog log)
        {
            _unitOfWork = unitOfWork;
            _log = log;
        }
        public void Handle(IAddPatientPhoneCommand command)
        {
            try
            {
                Check.NotNull(command, nameof(command));
                var phone = new PatientPhone
                {
                    PatientPhoneId =command.PatientPhoneId,
                    PatientId = command.PatientId,
                    PhoneNumber = command.Phone,
                    CreateBy = command.CreateBy,
                    CreatedAt = DateTime.Now
                };

                var repository = _unitOfWork.Repository<IPatientRepository>();
                repository.AddPatientPhone(phone);
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
