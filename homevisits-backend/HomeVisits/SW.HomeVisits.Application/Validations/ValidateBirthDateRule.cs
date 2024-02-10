using System;
using System.Threading.Tasks;
using SW.Framework.Validation;
using SW.HomeVisits.Application.Abstract.Commands;
using SW.HomeVisits.Application.Abstract.Validations;

namespace SW.HomeVisits.Application.Validations
{
    public class ValidateBirthDateRule : IValidationRule<ICreateChemistCommand>
    {
        public ValidateBirthDateRule()
        {
        }

        public Task<(bool IsValid, int ErrorCode)> Validate(ICreateChemistCommand command)
        {
            if (command.BirthDate.Date < DateTime.Now.Date)
                return ValidationRuleResult.Success();
            else
                return ValidationRuleResult.Fail(ErrorCodes.DateTimeGreaterThanToday);
        }
    }
}

