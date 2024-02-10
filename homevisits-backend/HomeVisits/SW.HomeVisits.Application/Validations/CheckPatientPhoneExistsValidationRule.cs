using System;
using System.Threading.Tasks;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.Validations
{
    public class CheckPatientPhoneExistsValidationRule : IValidationRule<IAddPatientPhoneCommand>
    {
        private readonly IPatientRepository _repository;

        public CheckPatientPhoneExistsValidationRule(IPatientRepository repository)
        {
            _repository = repository;
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(IAddPatientPhoneCommand command)
        {
            if (!_repository.PatientHasPhone(command.PatientId,command.Phone))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.PhoneNumberAlreadyExists);
        }
    }
}

