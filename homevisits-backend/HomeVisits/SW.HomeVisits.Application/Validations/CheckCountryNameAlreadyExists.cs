using System;
using System.Threading.Tasks;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.Validations
{
    public class CheckCountryNameAlreadyExists
        : IValidationRule<ICreateCountryCommand>, IValidationRule<IUpdateCountryCommand>
    {
        private readonly ICountryRepository _repository;

        public CheckCountryNameAlreadyExists(ICountryRepository repository)
        {
            _repository = repository;
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(ICreateCountryCommand command)
        {
            if (!_repository.CountryNameExists(command.CountryNameEn, command.ClientId,command.CountryId))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.CountryNameAlreadyExists);
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(IUpdateCountryCommand command)
        {
            if (!_repository.CountryNameExists(command.CountryNameEn, command.ClientId, command.CountryId))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.CountryNameAlreadyExists);
        }
    }
}
