using System;
using System.Threading.Tasks;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.Validations
{
    public class CheckGovernateNameAlreadyExists
        : IValidationRule<ICreateGovernateCommand>, IValidationRule<IUpdateGovernateCommand>
    {
        private readonly IGovernatRepository _repository;

        public CheckGovernateNameAlreadyExists(IGovernatRepository repository)
        {
            _repository = repository;
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(ICreateGovernateCommand command)
        {
            if (!_repository.GovernateNameExists(command.GovernateNameEn, command.CountryId,command.GovernateId))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.GovernateNameAlreadyExists);
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(IUpdateGovernateCommand command)
        {
            if (!_repository.GovernateNameExists(command.GovernateNameEn, command.CountryId, command.GovernateId))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.GovernateNameAlreadyExists);
        }
    }
}
