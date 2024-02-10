using System;
using System.Security;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace SW.Framework.Security
{
    public class AuthorisationManager : IAuthorisationManager
    {
        private readonly IServiceProvider _serviceProvider;

        public AuthorisationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Authorize<TMessage>(TMessage authenticatedMessage) where TMessage : class
        {

            //if (Thread.CurrentPrincipal == null)
            //    throw new SecurityException("The current thread has no identity to be used for authorisation.");
            var rules = _serviceProvider.GetServices(typeof(IAuthorisationRule<TMessage>));
            foreach (IAuthorisationRule<TMessage> rule in rules)
            {
                var result = rule.IsAuthorized(authenticatedMessage).Result;
                if (!result.IsAuthorized)
                {
                    var ex = new SecurityException(result.ErrorCode);

                    if (result.ExceptionData != null)
                    {
                        foreach (var item in result.ExceptionData)
                        {
                            if (ex.Data.Contains(item.Key))
                                ex.Data[item.Key] = item.Value;
                            else
                                ex.Data.Add(item.Key, item.Value);
                        }
                    }
                    throw ex;

                }
            }

        }
    }
}