using System;
using System.Threading.Tasks;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Validations;
using SW.HomeVisits.Domain.Repositories;

namespace SW.HomeVisits.Application.Validations
{
    public class CheckRoleNameAlreadyExists
        : IValidationRule<ICreateRoleCommand>, IValidationRule<IUpdateRoleCommand>
    {
        private readonly IRoleRepository _repository;

        public CheckRoleNameAlreadyExists(IRoleRepository repository)
        {
            _repository = repository;
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(ICreateRoleCommand command)
        {
            if (!_repository.RoleNameExists(command.NameAr, command.ClientId,command.RoleId))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.RoleNameAlreadyExists);
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(IUpdateRoleCommand command)
        {
            if (!_repository.RoleNameExists(command.NameAr, command.Client, command.RoleId))
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.RoleNameAlreadyExists);
        }
    }
}
