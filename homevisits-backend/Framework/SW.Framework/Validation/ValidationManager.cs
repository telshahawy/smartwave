using System;
using Microsoft.Extensions.DependencyInjection;
using SW.Framework.Exceptions;

namespace SW.Framework.Validation
{
    public class ValidationManager : IValidationManager
    {
        private readonly IServiceProvider _provider;

        public ValidationManager(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void Validate<TMessage>(TMessage validatedMessage) where TMessage : class
        {
            var rules = _provider.GetServices(typeof(IValidationRule<TMessage>));
            foreach (IValidationRule<TMessage> rule in rules)
            {
                var result = rule.Validate(validatedMessage).Result;
                if (!result.IsValid)
                {
                    throw new ValidationRuleException(result.ErrorCode, rule.GetType().FullName);
                }
            }

        }
    }
}